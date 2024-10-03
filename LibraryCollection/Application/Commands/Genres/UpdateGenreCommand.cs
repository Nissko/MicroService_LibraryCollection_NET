using MediatR;
using System.Runtime.Serialization;

namespace LibraryCollection.Application.Application.Commands.Genres
{
    public class UpdateGenreCommand
        : IRequest<bool>
    {
        [DataMember]
        public Guid Id { get; private set; }

        [DataMember]
        public string NameGenre { get; private set; }

        public UpdateGenreCommand(Guid id, string nameGenre)
        {
            Id = id;
            NameGenre = nameGenre;
        }
    }
}
