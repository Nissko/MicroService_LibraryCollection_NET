using EventBus.Abstractions;

namespace RentingOutBooksService.Application.Application.IntegrationEvents.Event.EventsToSendService
{
    public class RefundBookEvent
        : IIntegrationEvent
    {
        public Guid BookId { get; private set; }

        public RefundBookEvent(Guid bookId)
        {
            BookId = bookId;
        }
    }
}
