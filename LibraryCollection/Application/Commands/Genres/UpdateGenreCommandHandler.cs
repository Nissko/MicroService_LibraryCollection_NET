using LibraryCollection.Application.Common.Interfaces;
using LibraryCollection.Application.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LibraryCollection.Application.Application.Commands.Genres
{
    public class UpdateGenreCommandHandler
        : IRequestHandler<UpdateGenreCommand, bool>
    {
        private readonly IBooksContext _context;

        public UpdateGenreCommandHandler(IBooksContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(UpdateGenreCommand command, CancellationToken cancellationToken)
        {
            var genreContext = await _context.Genres.Where(t => t.Id == command.Id).FirstOrDefaultAsync();

            var result = genreContext ?? throw new BookApplicationException("Не удалось изменить жанр");

            result.UpdateExistingGenre(command.NameGenre);
            await _context.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
