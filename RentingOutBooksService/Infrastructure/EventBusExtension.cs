using EventBus.Abstractions;

namespace RentingOutBooksService.API.Infrastructure
{
    public static class EventBusExtension
    {
        public static IApplicationBuilder SubscribeToEvents(this IApplicationBuilder app)
        {
            var eventBus = app.ApplicationServices.GetRequiredService<IEventBusConsumer>();

            //eventBus.Subscribe<ChangeBookEvent, ChangeBookEventHandler>();

            return app;
        }
    }
}
