using LibraryCollection.Application.Common.Interfaces;
using LibraryCollection.Application.Exceptions;
using LibraryCollection.Domain.Aggregates.BookAggregate;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LibraryCollection.Application.Application.Commands.Genres
{
    public class FindAllGenreCommandHandler
        : IRequestHandler<FindAllGenreCommand, IEnumerable<BookGenre>>
    {
        private readonly IBooksContext _context;

        public FindAllGenreCommandHandler(IBooksContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<BookGenre>> Handle(FindAllGenreCommand command, CancellationToken cancellationToken)
        {
            var allGenres = _context.Genres.Where(t => t.BookId == command.BookId).ToListAsync();

            var result = allGenres ?? throw new BookApplicationException("Не удалось найти жанры");

            return await allGenres;
        }
    }
}
