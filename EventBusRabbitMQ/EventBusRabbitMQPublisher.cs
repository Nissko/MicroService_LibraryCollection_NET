using EventBus.Abstractions;
using RabbitMQ.Client;
using System.Text.Json;

namespace EventBusRabbitMQ
{
    public class EventBusRabbitMQPublisher : IEventBusPublisher
    {
        private readonly IRabbitMQPersistentConnection _persistentConnection;
        private readonly IEventBusSubscriptionsManager _subsManager;

        private IModel _channel;
        private string _brokerName;

        public EventBusRabbitMQPublisher(IRabbitMQPersistentConnection persistentConnection, IEventBusSubscriptionsManager subsManager, string brokerName)
        {
            _persistentConnection = persistentConnection ?? throw new ArgumentNullException(nameof(persistentConnection));
            _subsManager = subsManager;
            _brokerName = brokerName;
            _channel = CreateChannel();
        }

        public void Publish(IIntegrationEvent @event)
        {
            if (!_persistentConnection.IsConnected)
            {
                _persistentConnection.TryConnect();
            }

            var eventName = @event.GetType().Name;

            using var channel = _persistentConnection.CreateModel();
            var body = JsonSerializer.SerializeToUtf8Bytes(@event, @event.GetType(), new JsonSerializerOptions
            {
                WriteIndented = true
            });

            channel.BasicPublish(exchange: _brokerName,
                    routingKey: eventName,
                    mandatory: false,
                    basicProperties: null,
                    body: body);
        }

        public void Dispose()
        {
            if (_channel != null)
            {
                _channel.Dispose();
            }

            _subsManager.Clear();
        }

        private IModel CreateChannel()
        {
            if (!_persistentConnection.IsConnected)
            {
                _persistentConnection.TryConnect();
            }

            var channel = _persistentConnection.CreateModel();

            channel.CallbackException += (sender, ea) =>
            {
                _channel.Dispose();
                _channel = CreateChannel();
            };

            return channel;
        }
    }
}
