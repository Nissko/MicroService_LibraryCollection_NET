using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RentingOutBooksService.Domain.Aggregates.RentAggregate;

namespace RentingOutBooksService.Infrastructure.Configurations
{
    class RentStatusConfiguration
        : IEntityTypeConfiguration<RentStatus>
    {
        public void Configure(EntityTypeBuilder<RentStatus> rentStatusConfiguration)
        {
            rentStatusConfiguration.ToTable("RentStatuses");

            rentStatusConfiguration
                .Property(o => o.Id)
                .ValueGeneratedNever();

            rentStatusConfiguration
                .Property(o => o.Name)
                .HasMaxLength(200);
        }
    }
}
