using EventBus.Abstractions;

namespace LibraryCollection.Application.Application.IntegrationEvents.Event
{
    public class ChangeBookEvent
        : IIntegrationEvent
    {
        public ChangeBookEvent(Guid bookId)
        {
            BookId = bookId;
        }

        public Guid BookId { get; private set; }
    }
}
