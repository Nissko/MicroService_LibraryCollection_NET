using EventBus.Abstractions;
using LibraryCollection.Application.Application.IntegrationEvents.Event;
using LibraryCollection.Application.Application.IntegrationEvents.EventHandler;


namespace LibraryCollection.API.Infrastructure
{
    public static class EventBusExtension
    {
        public static IApplicationBuilder SubscribeToEvents(this IApplicationBuilder app)
        {
            var eventBus = app.ApplicationServices.GetRequiredService<IEventBusConsumer>();

            eventBus.Subscribe<ChangeBookEvent, ChangeBookEventHandler>();
            eventBus.Subscribe<RefundBookEvent, RefundBookEventHandler>();

            return app;
        }
    }
}
