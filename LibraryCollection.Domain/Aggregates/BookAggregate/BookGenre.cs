using LibraryCollection.Domain.Common;
using LibraryCollection.Domain.Exceptions;
using System.ComponentModel.DataAnnotations;

namespace LibraryCollection.Domain.Aggregates.BookAggregate
{
    public class BookGenre
        :Entity
    {
        /// <summary>
        /// Название жанра
        /// </summary>
        [Required]
        private string _nameGenre;
        public string NameGenre => _nameGenre;

        /// <summary>
        /// Внешний ключ книги
        /// </summary>
        public Guid BookId { get; private set; }

        public BookGenre(string nameGenre, Guid bookId)
        {
            _nameGenre = !string.IsNullOrWhiteSpace(nameGenre) ? nameGenre : throw new BookDomainException(nameof(nameGenre));
            BookId = bookId;
        }

        /// <summary>
        /// Вывод названия жанра
        /// </summary>
        public string GetGenreName() => _nameGenre;

        /// <summary>
        /// Функция обновления жанра
        /// </summary>
        public void UpdateExistingGenre(string? nameGenre)
        {
            _nameGenre = nameGenre ?? _nameGenre;
        }
    }
}
