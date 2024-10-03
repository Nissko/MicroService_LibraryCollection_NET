using LibraryCollection.Application.Application.Models.Books.Create;
using LibraryCollection.Application.DTO;

namespace LibraryCollection.Application.Application.Extensions
{
    public static class QuoteItemExtensions
    {
        public static IEnumerable<QuoteItemDTO> ToQuoteItemsDTO(this IEnumerable<QuoteItem> items)
        {
            foreach (var item in items)
            {
                yield return item.ToQuoteItemDTO();
            }
        }

        public static QuoteItemDTO ToQuoteItemDTO(this QuoteItem item)
        {
            return new QuoteItemDTO()
            {
                NameQuote = item.NameQuote,
            };
        }
    }
}
