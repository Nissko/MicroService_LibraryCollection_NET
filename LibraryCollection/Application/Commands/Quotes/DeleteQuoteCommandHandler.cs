using LibraryCollection.Application.Common.Interfaces;
using LibraryCollection.Application.Exceptions;
using MediatR;

namespace LibraryCollection.Application.Application.Commands.Quotes
{
    public class DeleteQuoteCommandHandler
        : IRequestHandler<DeleteQuoteCommand, bool>
    {
        private readonly IBooksContext _context;

        public DeleteQuoteCommandHandler(IBooksContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(DeleteQuoteCommand command, CancellationToken cancellationToken)
        {
            var quoteContext = await _context.Quotes.FindAsync(command.Id);

            var result = quoteContext ?? throw new BookApplicationException("Не удалось найти цитату");

            _context.Quotes.Remove(result);
            await _context.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
