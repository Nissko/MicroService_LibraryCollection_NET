using LibraryCollection.Application.Common.Interfaces;
using LibraryCollection.Domain.Aggregates.BookAggregate;
using MediatR;

namespace LibraryCollection.Application.Application.Commands.Books
{
    public class CreateBookCommandHandler
        : IRequestHandler<CreateBookCommand, bool>
    {
        private readonly IBooksContext _context;

        public CreateBookCommandHandler(IBooksContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(CreateBookCommand command, CancellationToken cancellationToken)
        {
            var book = new Book(command.Name, command.Description,
                command.NumberOfPage, command.AgeRestrict,
                command.ReleaseDate, command.Price, command.Discount);

            //Добавление категории
            book.SetCategory(command.CategoryName);

            //Добавление жанров
            foreach (var item in command.GenreItems)
            {
                book.AddGenres(item.NameGenre, book.Id);
            }

            //Добавление цитат
            foreach (var item in command.QuoteItems)
            {
                book.AddQuotes(item.NameQuote, book.Id);
            }

            _context.Books.Add(book);

            await _context.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
