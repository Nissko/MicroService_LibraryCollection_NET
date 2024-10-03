using LibraryCollection.Domain.Common;
using LibraryCollection.Domain.Exceptions;

namespace LibraryCollection.Domain.Aggregates.BookAggregate
{
    public class BookStatus
        : Enumeration
    {
        public static BookStatus Rent = new BookStatus(Guid.Parse("d088174f-6db7-458a-a596-37f27d365629")
                                                                 , "в аренде");
        public static BookStatus Free = new BookStatus(Guid.Parse("ffc5f778-3e06-4a5f-b608-680e6834ff49")
                                                                 , "доступна");

        public BookStatus(Guid id, string name)
        : base(id, name)
        {
        }

        public static IEnumerable<BookStatus> List() =>
        new[] { Rent, Free };

        public static BookStatus FromName(string name)
        {
            var state = List()
                .SingleOrDefault(s => string.Equals(s.Name, name, StringComparison.CurrentCultureIgnoreCase));

            if (state == null)
            {
                throw new BookDomainException($"Возможные значения для статуса книги: {string.Join(",", List().Select(s => s.Name))}");
            }

            return state;
        }

        public static BookStatus From(Guid id)
        {
            var state = List().SingleOrDefault(s => s.Id == id);

            if (state == null)
            {
                throw new BookDomainException($"Возможные значения для статуса книги: {string.Join(",", List().Select(s => s.Name))}");
            }

            return state;
        }
    }
}
