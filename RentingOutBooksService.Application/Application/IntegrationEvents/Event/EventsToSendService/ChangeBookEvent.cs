using EventBus.Abstractions;

namespace RentingOutBooksService.Application.Application.IntegrationEvents.Event.EventsToSendService
{
    public class ChangeBookEvent : IIntegrationEvent
    {
        public Guid BookId { get; private set; }

        public ChangeBookEvent(Guid bookId)
        {
            BookId = bookId;
        }
    }
}
