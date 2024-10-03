namespace LibraryCollection.Application.Application.Template.QuoteRequest
{
    public record CreateQuoteRequest(
        Guid bookId,
        string NameGenre
    );
}
