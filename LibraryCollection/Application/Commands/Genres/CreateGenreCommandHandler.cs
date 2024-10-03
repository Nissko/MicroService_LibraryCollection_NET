using LibraryCollection.Application.Common.Interfaces;
using LibraryCollection.Application.Exceptions;
using LibraryCollection.Domain.Aggregates.BookAggregate;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LibraryCollection.Application.Application.Commands.Genres
{
    public class CreateGenreCommandHandler
        : IRequestHandler<CreateGenreCommand, bool>
    {
        private readonly IBooksContext _context;

        public CreateGenreCommandHandler(IBooksContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(CreateGenreCommand command, CancellationToken cancellationToken)
        {
            var bookContext = await _context.Books.Where(t => t.Id == command.BookId).FirstOrDefaultAsync();

            var result = bookContext ?? throw new BookApplicationException("Не удалось найти книгу");

            var NewGenre = new BookGenre(command.NameGenre, result.Id);

            _context.Genres.Add(NewGenre);
            await _context.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
