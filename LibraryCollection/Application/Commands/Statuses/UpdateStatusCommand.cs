using MediatR;
using System.Runtime.Serialization;

namespace LibraryCollection.Application.Application.Commands.Statuses
{
    public class UpdateStatusCommand
        : IRequest<bool>
    {
        [DataMember]
        public Guid BookId { get; private set; }

        [DataMember]
        public string Type { get; private set; }

        public UpdateStatusCommand(Guid bookId, string type)
        {
            BookId = bookId;
            Type = type;
        }
    }
}
