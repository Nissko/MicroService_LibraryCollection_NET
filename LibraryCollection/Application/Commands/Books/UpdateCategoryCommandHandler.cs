using LibraryCollection.Application.Common.Interfaces;
using LibraryCollection.Application.Exceptions;
using MediatR;

namespace LibraryCollection.Application.Application.Commands.Books
{
    public class UpdateCategoryCommandHandler
        : IRequestHandler<UpdateCategoryCommand, bool>
    {
        private readonly IBooksContext _context;

        public UpdateCategoryCommandHandler(IBooksContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(UpdateCategoryCommand command, CancellationToken cancellationToken)
        {
            var bookContext = await _context.Books.FindAsync(command.BookId);

            var result = bookContext ?? throw new BookApplicationException("Не удалось изменить категорию");

            result.ChangeCategory(command.CategoryName);
            await _context.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
