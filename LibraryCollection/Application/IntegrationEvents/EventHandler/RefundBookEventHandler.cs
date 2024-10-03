using EventBus.Abstractions;
using LibraryCollection.Application.Application.Commands.Statuses;
using LibraryCollection.Application.Application.IntegrationEvents.Event;
using LibraryCollection.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LibraryCollection.Application.Application.IntegrationEvents.EventHandler
{
    public class RefundBookEventHandler
        : IIntegrationEventHandler<RefundBookEvent>
    {
        private readonly IMediator _mediator;
        private readonly IBooksContext _context;

        public RefundBookEventHandler(IMediator mediator, IBooksContext context)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task Handle(RefundBookEvent @event)
        {
            var bookContext = await _context.Books.Where(t => t.Id == @event.BookId).FirstOrDefaultAsync();

            if (bookContext == null) { throw new ArgumentNullException(nameof(bookContext)); }

            var changeBook = new UpdateStatusCommand(@event.BookId, "free");

            /*Изменяем статус книги*/
            await _mediator.Send(changeBook);
        }
    }
}
