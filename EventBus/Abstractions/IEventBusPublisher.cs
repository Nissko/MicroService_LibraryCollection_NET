namespace EventBus.Abstractions
{
    public interface IEventBusPublisher
    {
        void Publish(IIntegrationEvent @event);
    }
}
