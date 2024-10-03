using LibraryCollection.Domain.Aggregates.BookAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibraryCollection.Infrastructure.Configurations
{
    class BookConfiguration
        : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> bookConfiguration)
        {
            bookConfiguration.ToTable("Books");

            bookConfiguration.HasKey(o => o.Id);

            bookConfiguration
                .Property<string>("_name")
                .HasColumnName("Name");

            bookConfiguration
                .Property<string>("_description")
                .HasColumnName("Description");

            bookConfiguration
                .Property<int>("_numberOfPage")
                .HasColumnName("NumberOfPage");

            bookConfiguration
                .Property<int>("_ageRestrict")
                .HasColumnName("AgeRestrict");

            bookConfiguration
                .Property<DateTime>("_releaseDate")
                .HasColumnName("ReleaseDate");

            bookConfiguration
                .Property<decimal>("_price")
                .HasColumnName("Price");

            bookConfiguration
                .Property<decimal?>("_discount")
                .HasColumnName("Discount");

            bookConfiguration.HasOne(s => s.Category)
                .WithMany()
                .HasForeignKey("_categoryId");

            bookConfiguration.HasOne(s => s.Status)
                .WithMany()
                .HasForeignKey("_statusId");
        }
    }
}
