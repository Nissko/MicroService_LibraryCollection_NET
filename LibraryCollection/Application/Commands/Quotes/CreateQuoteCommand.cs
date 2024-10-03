using MediatR;
using System.Runtime.Serialization;

namespace LibraryCollection.Application.Application.Commands.Quotes
{
    public class CreateQuoteCommand
        : IRequest<bool>
    {
        [DataMember]
        public string NameQuote { get; private set; }

        [DataMember]
        public Guid BookId { get; private set; }

        public CreateQuoteCommand(string nameQuote, Guid bookId)
        {
            NameQuote = nameQuote;
            BookId = bookId;
        }
    }
}
