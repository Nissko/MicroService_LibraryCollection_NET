using LibraryCollection.Application.Common.Interfaces;
using LibraryCollection.Application.Exceptions;
using MediatR;

namespace LibraryCollection.Application.Application.Commands.Books
{
    public class UpdateBookCommandHandler
        : IRequestHandler<UpdateBookCommand, bool>
    {
        private readonly IBooksContext _context;

        public UpdateBookCommandHandler(IBooksContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(UpdateBookCommand command, CancellationToken cancellationToken)
        {
            var bookContext = await _context.Books.FindAsync(command.Id);

            var result = bookContext ?? throw new BookApplicationException("Не удалось отредактировать книгу");

            result.UpdateExistingBook(command.Name, command.Description, command.NumberOfPage,
                    command.AgeRestrict, command.ReleaseDate, command.Price, command.Discount);
            await _context.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
