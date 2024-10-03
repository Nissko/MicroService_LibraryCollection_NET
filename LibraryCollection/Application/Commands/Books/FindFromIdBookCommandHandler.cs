using LibraryCollection.Application.Common.Interfaces;
using LibraryCollection.Application.Exceptions;
using LibraryCollection.Domain.Aggregates.BookAggregate;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LibraryCollection.Application.Application.Commands.Books
{
    public class FindFromIdBookCommandHandler
        : IRequestHandler<FindFromIdBookCommand, IEnumerable<Book>>
    {
        private readonly IBooksContext _context;

        public FindFromIdBookCommandHandler(IBooksContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Book>> Handle(FindFromIdBookCommand command, CancellationToken cancellationToken)
        {
            var bookContext = _context.Books.Where(t => t.Id == command.BookId)
                                            .Include(t => t.Status).Include(t => t.Quotes)
                                            .Include(t => t.Category).Include(t => t.Genres).ToList();

            var result = bookContext ?? throw new BookApplicationException("Данная книга не найдена");

            return result;
        }
    }
}
