using LibraryCollection.Application.Common.Interfaces;
using LibraryCollection.Application.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LibraryCollection.Application.Application.Commands.Quotes
{
    public class UpdateQuoteCommandHandler
        : IRequestHandler<UpdateQuoteCommand, bool>
    {
        private readonly IBooksContext _context;

        public UpdateQuoteCommandHandler(IBooksContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(UpdateQuoteCommand command, CancellationToken cancellationToken)
        {
            var quoteContext = await _context.Quotes.Where(t => t.Id == command.Id).FirstOrDefaultAsync();

            var result = quoteContext ?? throw new BookApplicationException("Не удалось найти цитату");

            result.UpdateExistingQuote(command.NameQuote);
            await _context.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
