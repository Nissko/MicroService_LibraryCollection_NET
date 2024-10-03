using LibraryCollection.Application.Common.Interfaces;
using LibraryCollection.Application.Exceptions;
using MediatR;

namespace LibraryCollection.Application.Application.Commands.Books
{
    public class DeleteBookCommandHandler
        : IRequestHandler<DeleteBookCommand, bool>
    {
        private readonly IBooksContext _context;

        public DeleteBookCommandHandler(IBooksContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(DeleteBookCommand command, CancellationToken cancellationToken)
        {
            var bookContext = await _context.Books.FindAsync(command.Id);

            var result = bookContext ?? throw new BookApplicationException("Не удалось найти книгу");

            _context.Books.Remove(result);
            await _context.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
