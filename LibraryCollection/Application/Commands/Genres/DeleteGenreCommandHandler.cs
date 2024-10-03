using LibraryCollection.Application.Common.Interfaces;
using LibraryCollection.Application.Exceptions;
using MediatR;

namespace LibraryCollection.Application.Application.Commands.Genres
{
    public class DeleteGenreCommandHandler
        : IRequestHandler<DeleteGenreCommand, bool>
    {
        private readonly IBooksContext _context;

        public DeleteGenreCommandHandler(IBooksContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(DeleteGenreCommand command, CancellationToken cancellationToken)
        {
            var genreContext = await _context.Genres.FindAsync(command.Id);

            var result = genreContext ?? throw new BookApplicationException("Не удалось найти жанр");

            _context.Genres.Remove(result);
            await _context.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
