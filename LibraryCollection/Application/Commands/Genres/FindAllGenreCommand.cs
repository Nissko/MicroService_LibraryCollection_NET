using LibraryCollection.Domain.Aggregates.BookAggregate;
using MediatR;
using System.Runtime.Serialization;

namespace LibraryCollection.Application.Application.Commands.Genres
{
    public class FindAllGenreCommand
        : IRequest<IEnumerable<BookGenre>>
    {
        [DataMember]
        public Guid BookId { get; private set; }

        public FindAllGenreCommand(Guid bookId)
        {
            BookId = bookId;
        }
    }
}
