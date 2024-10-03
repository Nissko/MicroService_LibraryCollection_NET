using MediatR;
using System.Runtime.Serialization;

namespace LibraryCollection.Application.Application.Commands.Quotes
{
    public class UpdateQuoteCommand
        : IRequest<bool>
    {
        [DataMember]
        public Guid Id { get; private set; }

        [DataMember]
        public string NameQuote { get; private set; }

        public UpdateQuoteCommand(Guid id, string nameQuote)
        {
            Id = id;
            NameQuote = nameQuote;
        }
    }
}
