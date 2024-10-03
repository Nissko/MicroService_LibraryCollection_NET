using LibraryCollection.Domain.Aggregates.BookAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibraryCollection.Infrastructure.Configurations
{
    class QuoteConfiguration
        : IEntityTypeConfiguration<BookQuote>
    {
        public void Configure(EntityTypeBuilder<BookQuote> quoteConfiguration)
        {
            quoteConfiguration.ToTable("BookQuotes");

            quoteConfiguration.HasKey(q => q.Id);

            quoteConfiguration.Property<Guid>("BookId");

            quoteConfiguration
                .Property("_nameQuote")
                .HasColumnName("NameQuote");
        }
    }
}
