using LibraryCollection.Domain.Aggregates.BookAggregate;
using MediatR;
using System.Runtime.Serialization;

namespace LibraryCollection.Application.Application.Commands.Quotes
{
    public class FindAllQuoteCommand
        : IRequest<IEnumerable<BookQuote>>
    {
        [DataMember]
        public Guid BookId { get; private set; }

        public FindAllQuoteCommand(Guid bookId)
        {
            BookId = bookId;
        }
    }
}
