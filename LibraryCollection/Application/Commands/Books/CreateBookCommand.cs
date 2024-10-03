using LibraryCollection.Application.Application.Extensions;
using LibraryCollection.Application.Application.Models.Books.Create;
using LibraryCollection.Application.DTO;
using MediatR;
using System.Runtime.Serialization;

namespace LibraryCollection.Application.Application.Commands.Books
{
    public class CreateBookCommand
        : IRequest<bool>
    {
        private readonly List<GenreItemDTO> _genreItems;
        private readonly List<QuoteItemDTO> _quoteItems;

        [DataMember]
        public string Name { get; private set; }

        [DataMember]
        public string Description { get; private set; }

        [DataMember]
        public int NumberOfPage { get; private set; }

        [DataMember]
        public int AgeRestrict { get; private set; }

        [DataMember]
        public DateTime ReleaseDate { get; private set; }

        [DataMember]
        public decimal Price { get; private set; }

        [DataMember]
        public decimal? Discount { get; private set; }

        [DataMember]
        public string CategoryName { get; private set; }

        [DataMember]
        public IEnumerable<GenreItemDTO> GenreItems => _genreItems;

        [DataMember]
        public IEnumerable<QuoteItemDTO> QuoteItems => _quoteItems;

        public CreateBookCommand()
        {
            _genreItems = new List<GenreItemDTO>();
            _quoteItems = new List<QuoteItemDTO>();
        }

        public CreateBookCommand(/***List<CategoryItem> categoryItems, */List<GenreItem> genreItems,
            List<QuoteItem> quoteItems, string name, string description,
            int numberOfPage, int ageRestrict, DateTime releaseDate,
            decimal price, decimal? discount, string categoryName) : this()
        {
            //**_categoryItems = categoryItems.ToCategoryItemsDTO().ToList();
            _genreItems = genreItems.ToGenreItemsDTO().ToList();
            _quoteItems = quoteItems.ToQuoteItemsDTO().ToList();
            Name = name;
            Description = description;
            NumberOfPage = numberOfPage;
            AgeRestrict = ageRestrict;
            ReleaseDate = releaseDate;
            Price = price;
            Discount = discount;
            CategoryName = categoryName;
        }

    }
}
