using LibraryCollection.Domain.Common;
using LibraryCollection.Domain.Exceptions;

namespace LibraryCollection.Domain.Aggregates.BookAggregate
{
    public abstract class BookCategory
        : Enumeration
    {
        public static BookCategory EasyReading = new BookCategory(Guid.Parse("a06d00df-593b-4558-9167-702095e0bd47"),
                                                        "Легкое чтение".ToLowerInvariant());
        public static BookCategory SeriousReading = new BookCategory(Guid.Parse("db7392b1-6e55-400e-af92-c8ec3ca6bc2d"),
                                                        "Серьезное чтение".ToLowerInvariant());
        public static BookCategory History = new BookCategory(Guid.Parse("7ba0e4e3-b129-4998-996e-2baf2d02a88f"),
                                                        "История".ToLowerInvariant());
        public static BookCategory ChildrensBooks = new BookCategory(Guid.Parse("e05f2b79-d39c-46f1-a01f-f4a6810dad33"),
                                                        "Детские книги".ToLowerInvariant());
        
        public BookCategory(Guid id, string name)
        : base(id, name)
        {
        }

        public static IEnumerable<BookCategory> List() =>
        new[] { EasyReading, SeriousReading, History, ChildrensBooks };

        public static BookCategory FromName(string name)
        {
            var state = List()
                .SingleOrDefault(s => string.Equals(s.Name, name, StringComparison.CurrentCultureIgnoreCase));

            if (state == null)
            {
                throw new BookDomainException($"Возможные значения для категории: {string.Join(",", List().Select(s => s.Name))}");
            }

            return state;
        }

        public static BookCategory From(Guid id)
        {
            var state = List().SingleOrDefault(s => s.Id == id);

            if (state == null)
            {
                throw new BookDomainException($"Возможные значения для категории: {string.Join(",", List().Select(s => s.Name))}");
            }

            return state;
        }
    }
}
