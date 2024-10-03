using LibraryCollection.Domain.Common;
using LibraryCollection.Domain.Exceptions;
using System.ComponentModel.DataAnnotations;

namespace LibraryCollection.Domain.Aggregates.BookAggregate
{
    public class BookQuote
        : Entity
    {
        /// <summary>
        /// Текст цитаты
        /// </summary>
        [Required]
        private string _nameQuote;
        public string NameQuote => _nameQuote;

        /// <summary>
        /// Внешний ключ книги
        /// </summary>
        public Guid BookId { get; private set; }

        public BookQuote(string nameQuote, Guid bookId)
        {
            _nameQuote = !string.IsNullOrWhiteSpace(nameQuote) ? nameQuote : throw new BookDomainException(nameof(nameQuote));
            BookId = bookId;
        }

        /// <summary>
        /// Вывод текста цитаты
        /// </summary>
        public string GetBookQuoteName() => _nameQuote;

        /// <summary>
        /// Функция обновления цитаты
        /// </summary>
        public void UpdateExistingQuote(string? nameQuote)
        {
            _nameQuote = nameQuote ?? _nameQuote;
        }
    }
}
