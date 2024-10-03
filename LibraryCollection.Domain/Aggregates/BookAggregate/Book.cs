using LibraryCollection.Domain.Common;
using LibraryCollection.Domain.Exceptions;

namespace LibraryCollection.Domain.Aggregates.BookAggregate
{
    public class Book
        : Entity, IAggregateRoot
    {
        /*Название*/
        private string _name;
        public string Name => _name;

        /*Описание*/
        private string _description;
        public string Description => _description;

        /*Количество страниц*/
        private int _numberOfPage;
        public int NumberOfPages => _numberOfPage;

        /*Возрастные ограничения*/
        private int _ageRestrict;
        public int AgeRestrict => _ageRestrict;

        /*Дата выхода*/
        private DateTime _releaseDate;
        public DateTime ReleaseDate => _releaseDate;

        /*Цена*/
        private decimal _price;
        public decimal Price => _price;

        /*Размер скидки*/
        private decimal? _discount;
        public decimal? Discount => _discount;

        /*Статус книги*/
        public BookStatus Status { get; private set; }
        private Guid _statusId;

        /*Цитаты*/
        public IReadOnlyCollection<BookQuote> Quotes => _quotes;
        private List<BookQuote> _quotes;

        /*Категории*/
        public BookCategory Category { get; private set; }
        private Guid _categoryId;

        /*Жанры*/
        public IReadOnlyCollection<BookGenre> Genres => _genres;
        private List<BookGenre> _genres;


        protected Book()
        {
            _quotes = new List<BookQuote>();
            _genres = new List<BookGenre>();
        }

        public Book(string name, string description, int numberOfPage, int ageRestrict, 
            DateTime releaseDate, decimal price, decimal? discount) : this()
        {
            _name = name;
            _description = description;
            _numberOfPage = numberOfPage;
            _ageRestrict = ageRestrict;
            _releaseDate = releaseDate.ToUniversalTime();
            _price = price;
            _discount = discount;
            _statusId = BookStatus.Free.Id;
        }

        /*Добавление цитат*/
        public void AddQuotes(string nameQuote, Guid bookId)
        {
            var existingQuoteForBook = _quotes.Where(o => o.GetBookQuoteName() == nameQuote)
                .SingleOrDefault();

            if(existingQuoteForBook != null)
            {
                throw new BookDomainException("Такая цитата уже добавлена");
            }
            else
            {
                var bookQuote = new BookQuote(nameQuote, bookId);
                _quotes.Add(bookQuote);
            }
        }

        /// <summary>
        /// Добавление категории
        /// </summary>
        public void SetCategory(string nameCategory)
        {
            var category = BookCategory.FromName(nameCategory);

            _categoryId = category.Id;
        }

        /// <summary>
        /// Изменение категории
        /// </summary>
        public void ChangeCategory(string nameCategory)
        {
            var category = BookCategory.FromName(nameCategory);

            _categoryId = category.Id;
        }

        /// <summary>
        /// Добавление жанров
        /// </summary>
        public void AddGenres(string nameGenre, Guid bookId)
        {
            var existingGenreForBook = _genres.Where(o => o.GetGenreName() == nameGenre)
                .SingleOrDefault();

            if (existingGenreForBook != null)
            {
                throw new BookDomainException("Такой жанр уже добавлен");
            }
            else
            {
                var bookGenre = new BookGenre(nameGenre, bookId);
                _genres.Add(bookGenre);
            }
        }

        /// <summary>
        /// Изменение статуса книги
        /// </summary>
        public void SetRentStatus()
        {
            if (_statusId == BookStatus.Rent.Id)
            {
                throw new BookDomainException("Книга уже имеет статус - 'аренда'");
            }

            _statusId = BookStatus.Rent.Id;
        }

        public void SetFreeStatus()
        {
            if(_statusId == BookStatus.Free.Id)
            {
                throw new BookDomainException("Книга уже имеет статус - 'доступна'");
            }

            _statusId = BookStatus.Free.Id;
        }

        /// <summary>
        /// Изменение книги
        /// </summary>
        public void UpdateExistingBook(string? name, string? description, int? numberOfPage, int? ageRestrict,
            DateTime? releaseDate, decimal? price, decimal? discount)
        {
            _name = name ?? _name;
            _description = description ?? _description;
            _numberOfPage = numberOfPage ?? _numberOfPage;
            _ageRestrict = ageRestrict ?? _ageRestrict;
            _releaseDate = releaseDate ?? _releaseDate;
            _price = price ?? _price;
            _discount = discount ?? _discount;
        }
    }
}
