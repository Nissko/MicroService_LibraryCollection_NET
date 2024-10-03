namespace LibraryCollection.Application.Application.Template.CagetoryRequest
{
    public record UpdateCategoryBookRequest(
        Guid bookId,
        string Name
    );
}
