using EventBus.Abstractions;
using MediatR;

namespace EventBusServiceBus
{
    public class EventBusServiceBusPublisher : IEventBusPublisher
    {
        private readonly IMediator _mediator;

        public EventBusServiceBusPublisher(IMediator mediator)
        {
            _mediator = mediator;
        }

        public void Publish(IIntegrationEvent @event)
        {
            _mediator.Publish(@event);
        }
    }
}
