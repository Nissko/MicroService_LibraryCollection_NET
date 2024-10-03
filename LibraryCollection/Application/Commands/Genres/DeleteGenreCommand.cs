using MediatR;
using System.Runtime.Serialization;

namespace LibraryCollection.Application.Application.Commands.Genres
{
    public class DeleteGenreCommand
        : IRequest<bool>
    {
        [DataMember]
        public Guid Id { get; private set; }

        public DeleteGenreCommand(Guid id)
        {
            Id = id;
        }
    }
}
