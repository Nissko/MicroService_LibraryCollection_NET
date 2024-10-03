using LibraryCollection.Domain.Aggregates.BookAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibraryCollection.Infrastructure.Configurations
{
    public class StatusConfiguration
        : IEntityTypeConfiguration<BookStatus>
    {
        public void Configure(EntityTypeBuilder<BookStatus> bookStatusConfiguration)
        {
            bookStatusConfiguration.ToTable("BooksStatuses");

            bookStatusConfiguration
                .Property(o => o.Id)
                .ValueGeneratedNever();

            bookStatusConfiguration
                .Property(o => o.Name)
                .HasMaxLength(200);
        }
    }
}
