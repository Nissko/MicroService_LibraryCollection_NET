using LibraryCollection.Domain.Aggregates.BookAggregate;
using MediatR;
using System.Runtime.Serialization;

namespace LibraryCollection.Application.Application.Commands.Books
{
    public class FindFromIdBookCommand
        : IRequest<IEnumerable<Book>>
    {
        [DataMember]
        public Guid BookId { get; set; }

        public FindFromIdBookCommand(Guid bookId)
        {
            BookId = bookId;
        }
    }
}
