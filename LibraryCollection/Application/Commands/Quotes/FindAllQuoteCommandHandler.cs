using LibraryCollection.Application.Common.Interfaces;
using LibraryCollection.Application.Exceptions;
using LibraryCollection.Domain.Aggregates.BookAggregate;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LibraryCollection.Application.Application.Commands.Quotes
{
    public class FindAllQuoteCommandHandler
        : IRequestHandler<FindAllQuoteCommand, IEnumerable<BookQuote>>
    {
        private readonly IBooksContext _context;

        public FindAllQuoteCommandHandler(IBooksContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<BookQuote>> Handle(FindAllQuoteCommand command, CancellationToken cancellationToken)
        {
            var allQuotes = _context.Quotes.Where(t => t.BookId == command.BookId).ToListAsync();

            var result = allQuotes ?? throw new BookApplicationException("Не удалось найти цитаты у книги");

            return await allQuotes;
        }
    }
}
