using LibraryCollection.Application.Common.Interfaces;
using LibraryCollection.Application.Exceptions;
using MediatR;

namespace LibraryCollection.Application.Application.Commands.Statuses
{
    public class UpdateStatusCommandHandler
        : IRequestHandler<UpdateStatusCommand, bool>
    {
        private readonly IBooksContext _context;

        public UpdateStatusCommandHandler(IBooksContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(UpdateStatusCommand command, CancellationToken cancellationToken)
        {
            var bookContext = await _context.Books.FindAsync(command.BookId);

            var result = bookContext ?? throw new BookApplicationException("Не удалось найти книгу");

            if (command.Type == "rent")
            {
                result.SetRentStatus();
            }
            else
            {
                result.SetFreeStatus();
            }

            await _context.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
