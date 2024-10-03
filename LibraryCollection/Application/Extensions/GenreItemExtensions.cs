using LibraryCollection.Application.Application.Models.Books.Create;
using LibraryCollection.Application.DTO;

namespace LibraryCollection.Application.Application.Extensions
{
    public static class GenreItemExtensions
    {
        public static IEnumerable<GenreItemDTO> ToGenreItemsDTO(this IEnumerable<GenreItem> items)
        {
            foreach (var item in items)
            {
                yield return item.ToGenreItemDTO();
            }
        }

        public static GenreItemDTO ToGenreItemDTO(this GenreItem item)
        {
            return new GenreItemDTO()
            {
                NameGenre = item.NameGenre,
            };
        }
    }
}
