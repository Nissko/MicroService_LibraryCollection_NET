using MediatR;
using System.Runtime.Serialization;

namespace LibraryCollection.Application.Application.Commands.Books
{
    public class UpdateCategoryCommand
        : IRequest<bool>
    {
        [DataMember]
        public Guid BookId { get; private set; }

        [DataMember]
        public string CategoryName { get; private set; }

        public UpdateCategoryCommand(Guid bookId, string categoryName)
        {
            BookId = bookId;
            CategoryName = categoryName;
        }
    }
}
