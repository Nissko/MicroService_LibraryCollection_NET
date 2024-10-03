using MediatR;
using System.Runtime.Serialization;

namespace LibraryCollection.Application.Application.Commands.Genres
{
    public class CreateGenreCommand
        : IRequest<bool>
    {
        [DataMember]
        public string NameGenre { get; private set; }

        [DataMember]
        public Guid BookId { get; private set; }

        public CreateGenreCommand(string nameGenre, Guid bookId)
        {
            NameGenre = nameGenre;
            BookId = bookId;
        }
    }
}
