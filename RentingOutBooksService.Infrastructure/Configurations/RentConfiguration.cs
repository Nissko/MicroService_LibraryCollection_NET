using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RentingOutBooksService.Domain.Aggregates.RentAggregate;

namespace RentingOutBooksService.Infrastructure.Configurations
{
    class RentConfiguration
        : IEntityTypeConfiguration<Rent>
    {
        public void Configure(EntityTypeBuilder<Rent> rentConfiguration)
        {
            rentConfiguration.ToTable("Rents");

            rentConfiguration.HasKey(o => o.Id);

            rentConfiguration
                .Property<int>("_countRentDay")
                .HasColumnName("CountRentDay");

            rentConfiguration
                .Property<DateTime>("_rentDateStart")
                .HasColumnName("RentStartDate");

            rentConfiguration
                .Property<DateTime>("_rentDateEnd")
                .HasColumnName("RentEndDate");

            rentConfiguration
                .Property<Guid>("_clientId")
                .HasColumnName("ClientId");

            rentConfiguration
                .Property<Guid>("_rentStatusId")
                .HasColumnName("RentStatusId");

            rentConfiguration
                .Property<Guid>("_bookId")
                .HasColumnName("BookId");

            rentConfiguration.HasOne<Tenantry>()
                .WithMany()
                .HasForeignKey("_clientId");

            rentConfiguration.HasOne(s => s.RentStatus)
                .WithMany()
                .HasForeignKey("_rentStatusId");
        }
    }
}
