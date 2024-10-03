using LibraryCollection.Domain.Aggregates.BookAggregate;
using MediatR;

namespace LibraryCollection.Application.Application.Queries.Books
{
    public class ShowBooksQuery
        : IRequest<IEnumerable<Book>>
    { }
}
