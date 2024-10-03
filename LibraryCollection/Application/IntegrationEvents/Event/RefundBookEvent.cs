using EventBus.Abstractions;

namespace LibraryCollection.Application.Application.IntegrationEvents.Event
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
