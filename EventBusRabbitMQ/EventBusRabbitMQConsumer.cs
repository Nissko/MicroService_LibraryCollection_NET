using EventBus.Abstractions;
using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;
using Autofac;

namespace EventBusRabbitMQ
{
    public class EventBusRabbitMQConsumer : IEventBusConsumer, IDisposable
    {
        const string AUTOFAC_SCOPE_NAME = "library_event_bus";

        private readonly IRabbitMQPersistentConnection _persistentConnection;
        private readonly IEventBusSubscriptionsManager _subsManager;
        private readonly ILifetimeScope _autofac;

        private IModel _channel;
        private string _queueName;
        private string _brokerName;
        private string _retryBrokerName;
        private string _retryQueueName;

        public EventBusRabbitMQConsumer(IRabbitMQPersistentConnection persistentConnection, IEventBusSubscriptionsManager subsManager, ILifetimeScope autofac, string brokerName, string queueName)
        {
            _persistentConnection = persistentConnection ?? throw new ArgumentNullException(nameof(persistentConnection));
            _subsManager = subsManager;
            _autofac = autofac;
            _brokerName = brokerName;
            _queueName = queueName;
            _retryBrokerName = brokerName + "_retry";
            _retryQueueName = queueName + "_retry";
            _channel = CreateChannel();
        }

        private static string _numberOfRetriesField = "numberOfRetries";

        private readonly int _maxRetryCount = 3;

        public void Subscribe<T, TH>()
                where T : IIntegrationEvent
                where TH : IIntegrationEventHandler<T>
        {
            var eventName = _subsManager.GetEventKey<T>();
            DoInternalSubscription(eventName);

            _subsManager.AddSubscription<T, TH>();
            StartBasicConsume();
        }

        public void Unsubscribe<T, TH>()
            where T : IIntegrationEvent
            where TH : IIntegrationEventHandler<T>
        {
            _subsManager.RemoveSubscription<T, TH>();
        }

        public void Dispose()
        {
            if (_channel != null)
            {
                _channel.Dispose();
            }

            _subsManager.Clear();
        }

        private void DoInternalSubscription(string eventName)
        {
            var containsKey = _subsManager.HasSubscriptionsForEvent(eventName);
            if (!containsKey)
            {
                if (!_persistentConnection.IsConnected)
                {
                    _persistentConnection.TryConnect();
                }

                _channel.QueueBind(queue: _queueName,
                                    exchange: _brokerName,
                                    routingKey: eventName);

                _channel.QueueBind(queue: _retryQueueName,
                                    exchange: _retryBrokerName,
                                    routingKey: eventName);

                // TODO: Количество повторов в заголовке, TTL, переименовать мертвые сообщения в retry, 
                // организация очереди мертвых сообщений
            }
        }

        private IModel CreateChannel()
        {
            if (!_persistentConnection.IsConnected)
            {
                _persistentConnection.TryConnect();
            }

            Dictionary<string, object> retryPolicyForQuery = new()
            {
                { "x-dead-letter-exchange", _brokerName }
            };

            var channel = _persistentConnection.CreateModel();

            channel.ExchangeDeclare(exchange: _brokerName,
                                    type: ExchangeType.Direct);

            channel.QueueDeclare(queue: _queueName,
                                 durable: true,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

            channel.ExchangeDeclare(exchange: _retryBrokerName,
                                    type: ExchangeType.Direct);

            var queue = channel.QueueDeclare(queue: _retryQueueName,
                                 durable: true,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: retryPolicyForQuery);

            channel.CallbackException += (sender, ea) =>
            {
                _channel.Dispose();
                _channel = CreateChannel();
                StartBasicConsume();
            };

            return channel;
        }

        private void StartBasicConsume()
        {
            if (_channel != null)
            {
                var consumer = new AsyncEventingBasicConsumer(_channel);

                consumer.Received += Consumer_Received;

                _channel.BasicConsume(
                    queue: _queueName,
                    autoAck: false,
                    consumer: consumer);
            }
        }

        // TODO: Реализовать сохранение количества попыток обработки конкретного сообщения с проверкой и отменой сообщения
        // (можно кидать в очередь не успешных сообщений)
        private async Task Consumer_Received(object sender, BasicDeliverEventArgs eventArgs)
        {
            var eventName = eventArgs.RoutingKey;
            var numberOfRetries = GetNumberOfRetries(eventArgs.BasicProperties);

            var message = Encoding.UTF8.GetString(eventArgs.Body.Span);
            try
            {
                await ProcessEvent(eventName, message);
                _channel.BasicAck(eventArgs.DeliveryTag, multiple: false);
            }
            //catch(ProcessEventException peEx)
            catch (Exception ex)
            {
                numberOfRetries++;

                if (CanRetry(numberOfRetries))
                {
                    // TODO: Вынести ответ рэббиту, чтобы обработать AlreadyClosedException
                    _channel.BasicAck(eventArgs.DeliveryTag, multiple: false);
                    PublishMessageToRetryQueue(numberOfRetries, eventArgs);
                }
                else
                {
                    _channel.BasicNack(eventArgs.DeliveryTag, multiple: false, requeue: false);
                }
            }
        }

        /// <summary>
        /// Расчет времени ожидания для сообщения в очереди (TTL)
        /// </summary>
        /// <param name="numberOfRetries"></param>
        /// <returns></returns>
        private int CalculateTtl(int numberOfRetries)
        {
            return numberOfRetries == 0 ? 1000 : numberOfRetries * 10 * 1000;
        }

        private int GetNumberOfRetries(IBasicProperties basicProperties)
        {
            if (basicProperties.Headers is null || !basicProperties.Headers.ContainsKey(_numberOfRetriesField))
            {
                return 0;
            }

            return (int)basicProperties.Headers[_numberOfRetriesField];
        }

        private IBasicProperties FillRetryMessageProperties(int numberOfRetries, IBasicProperties basicProperties)
        {
            basicProperties.Headers = basicProperties.Headers ?? new Dictionary<string, object>();
            basicProperties.Headers[_numberOfRetriesField] = numberOfRetries;
            basicProperties.Expiration = CalculateTtl(numberOfRetries).ToString();
            return basicProperties;
        }

        private void PublishMessageToRetryQueue(int numberOfRetries, BasicDeliverEventArgs eventArgs)
        {
            var properties = FillRetryMessageProperties(numberOfRetries, eventArgs.BasicProperties);

            _channel.BasicPublish(exchange: _retryBrokerName,
                routingKey: eventArgs.RoutingKey,
                mandatory: false,
                basicProperties: properties,
                body: eventArgs.Body);
        }

        private bool CanRetry(int numberOfRetries)
        {
            return numberOfRetries < _maxRetryCount;
        }

        private async Task ProcessEvent(string eventName, string message)
        {
            if (_subsManager.HasSubscriptionsForEvent(eventName))
            {
                using (var scope = _autofac.BeginLifetimeScope(AUTOFAC_SCOPE_NAME))
                {
                    var subscriptions = _subsManager.GetHandlersForEvent(eventName);
                    foreach (var subscription in subscriptions)
                    {
                        var handler = scope.ResolveOptional(subscription.HandlerType);
                        if (handler == null) continue;
                        var eventType = _subsManager.GetEventTypeByName(eventName);
                        var integrationEvent = JsonSerializer.Deserialize(message, eventType, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
                        var concreteType = typeof(IIntegrationEventHandler<>).MakeGenericType(eventType);

                        // NOTE: Освобождаем поток для других задач
                        await Task.Yield();
                        await (Task)concreteType.GetMethod("Handle").Invoke(handler, new object[] { integrationEvent });
                    }
                }
            }
        }
    }
}
