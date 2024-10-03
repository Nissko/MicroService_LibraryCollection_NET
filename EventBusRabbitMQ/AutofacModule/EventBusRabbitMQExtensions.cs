using Autofac;
using EventBus.Abstractions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;

namespace EventBusRabbitMQ.AutofacModule
{
    public static class EventBusRabbitMQExtensions
    {
        public static IServiceCollection AddEventBusRabbitMQConnection(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IRabbitMQPersistentConnection>(sp =>
            {
                var factory = new ConnectionFactory()
                {
                    HostName = configuration["EventBusRabbitMQ:EventBusConnection"],
                    DispatchConsumersAsync = true,
                    Port = Int32.Parse(configuration["EventBusRabbitMQ:EventBusPort"]),
                    Ssl =
                    {
                        Enabled = false
                    },
                    AutomaticRecoveryEnabled = true
                };

                if (!string.IsNullOrEmpty(configuration["EventBusRabbitMQ:EventBusVirtualHost"]))
                {
                    factory.VirtualHost = configuration["EventBusRabbitMQ:EventBusVirtualHost"];
                }

                if (!string.IsNullOrEmpty(configuration["EventBusRabbitMQ:EventBusUserName"]))
                {
                    factory.UserName = configuration["EventBusRabbitMQ:EventBusUserName"];
                }

                if (!string.IsNullOrEmpty(configuration["EventBusRabbitMQ:EventBusPassword"]))
                {
                    factory.Password = configuration["EventBusRabbitMQ:EventBusPassword"];
                }

                var retryCount = 2;

                if (!string.IsNullOrEmpty(configuration["EventBusRabbitMQ:RetryCount"]))
                {
                    retryCount = Convert.ToInt32(configuration["EventBusRabbitMQ:RetryCount"]);
                }

                return new DefaultRabbitMQPersistentConnection(retryCount, factory);
            });

            return services;
        }

        public static IServiceCollection AddEventBusRabbitMQPublisher(this IServiceCollection services, string brokerName)
        {
            services.AddSingleton<IEventBusPublisher, EventBusRabbitMQPublisher>(sp =>
            {
                var rabbitMQPersistentConnection = sp.GetRequiredService<IRabbitMQPersistentConnection>();
                var eventBusSubcriptionsManager = sp.GetRequiredService<IEventBusSubscriptionsManager>();

                return new EventBusRabbitMQPublisher(rabbitMQPersistentConnection, eventBusSubcriptionsManager, brokerName);
            });

            return services;
        }

        public static IServiceCollection AddEventBusRabbitMQConsumer(this IServiceCollection services, string brokerName, string queueName)
        {
            services.AddSingleton<IEventBusConsumer, EventBusRabbitMQConsumer>(sp =>
            {
                var rabbitMQPersistentConnection = sp.GetRequiredService<IRabbitMQPersistentConnection>();
                var eventBusSubcriptionsManager = sp.GetRequiredService<IEventBusSubscriptionsManager>();
                var iLifetimeScope = sp.GetRequiredService<ILifetimeScope>();

                return new EventBusRabbitMQConsumer(rabbitMQPersistentConnection, eventBusSubcriptionsManager, iLifetimeScope, brokerName, queueName);
            });

            return services;
        }
    }
}
