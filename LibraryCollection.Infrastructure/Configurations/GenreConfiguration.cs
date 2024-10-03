using LibraryCollection.Domain.Aggregates.BookAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibraryCollection.Infrastructure.Configurations
{
    class GenreConfiguration
        : IEntityTypeConfiguration<BookGenre>
    {
        public void Configure(EntityTypeBuilder<BookGenre> genreConfiguration)
        {
            genreConfiguration.ToTable("BookGenres");

            genreConfiguration.HasKey(q => q.Id);

            genreConfiguration.Property<Guid>("BookId");

            genreConfiguration
                .Property("_nameGenre")
                .HasColumnName("NameGenre");
        }
    }
}
