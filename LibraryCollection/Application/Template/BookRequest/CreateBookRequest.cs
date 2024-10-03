using LibraryCollection.Application.Application.Models.Books.Create;

namespace LibraryCollection.Application.Application.Template.BookRequest
{
    public record CreateBookRequest(
        string Name,
        string Description,
        int NumberOfPage,
        int AgeRestrict,
        DateTime ReleaseDate,
        decimal Price,
        decimal? Discount,
        string CategoryName,
        List<GenreItem> ItemsGenre,
        List<QuoteItem> ItemsQuote
    );
}
