using LibraryCollection.Domain.Aggregates.BookAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibraryCollection.Infrastructure.Configurations
{
    class CategoryConfiguration
        : IEntityTypeConfiguration<BookCategory>
    {
        public void Configure(EntityTypeBuilder<BookCategory> bookStatusConfiguration)
        {
            bookStatusConfiguration.ToTable("BookCategories");

            bookStatusConfiguration
                .Property(o => o.Id)
                .ValueGeneratedNever();

            bookStatusConfiguration
                .Property(o => o.Name)
                .HasMaxLength(200);
        }
    }
}
