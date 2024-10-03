namespace LibraryCollection.Application.Application.Template.BookRequest
{
    public record UpdateBookRequest(
        Guid Id,
        string? Name,
        string? Description,
        int? NumberOfPage,
        int? AgeRestrict,
        DateTime? ReleaseDate,
        decimal? Price,
        decimal? Discount
    );
}
