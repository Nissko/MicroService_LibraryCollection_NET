using LibraryCollection.Application.Common.Interfaces;
using LibraryCollection.Domain.Aggregates.BookAggregate;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LibraryCollection.Application.Application.Queries.Books
{
    public class ShowBooksQueryHandler
        : IRequestHandler<ShowBooksQuery, IEnumerable<Book>>
    {
        private readonly IBooksContext _context;

        public ShowBooksQueryHandler(IBooksContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Book>> Handle(ShowBooksQuery query, CancellationToken cancellationToken)
        {
            var allBooks = _context.Books.Include(t => t.Category)
                                    .Include(t => t.Status)
                                    .Include(t => t.Genres)
                                    .Include(t => t.Quotes)
                                    .ToListAsync();

            if (allBooks == null)
            {
                throw new Exception("Не удалось вывести все книги");
            }

            return await allBooks;
        }
    }
}
