namespace LibraryCollection.Application.Application.Template.GenreRequest
{
    public record CreateGenreRequest(
        Guid bookId,
        string NameGenre
    );
}
