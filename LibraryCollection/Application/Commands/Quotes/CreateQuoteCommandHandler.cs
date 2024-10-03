using LibraryCollection.Application.Common.Interfaces;
using LibraryCollection.Application.Exceptions;
using LibraryCollection.Domain.Aggregates.BookAggregate;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LibraryCollection.Application.Application.Commands.Quotes
{
    public class CreateQuoteCommandHandler
        : IRequestHandler<CreateQuoteCommand, bool>
    {
        private readonly IBooksContext _context;

        public CreateQuoteCommandHandler(IBooksContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(CreateQuoteCommand command, CancellationToken cancellationToken)
        {
            var bookContext = await _context.Books.Where(t => t.Id == command.BookId).FirstOrDefaultAsync();

            var result = bookContext ?? throw new BookApplicationException("Не удалось найти книгу");

            var NewQuote = new BookQuote(command.NameQuote, result.Id);

            _context.Quotes.Add(NewQuote);
            await _context.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
