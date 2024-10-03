using MediatR;
using System.Runtime.Serialization;

namespace LibraryCollection.Application.Application.Commands.Quotes
{
    public class DeleteQuoteCommand
        : IRequest<bool>
    {
        [DataMember]
        public Guid Id { get; private set; }

        public DeleteQuoteCommand(Guid id)
        {
            Id = id;
        }
    }
}
