using MediatR;
using System.Runtime.Serialization;

namespace LibraryCollection.Application.Application.Commands.Books
{
    public class DeleteBookCommand
        : IRequest<bool>
    {
        [DataMember]
        public Guid Id { get; private set; }

        public DeleteBookCommand(Guid id)
        {
            Id = id;
        }
    }
}
