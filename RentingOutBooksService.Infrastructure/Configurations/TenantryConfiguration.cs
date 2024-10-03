using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RentingOutBooksService.Domain.Aggregates.RentAggregate;

namespace RentingOutBooksService.Infrastructure.Configurations
{
    class TenantryConfiguration
        : IEntityTypeConfiguration<Tenantry>
    {
        public void Configure(EntityTypeBuilder<Tenantry> tenantryConfiguration)
        {
            tenantryConfiguration.ToTable("Tenantries");

            tenantryConfiguration.HasKey(o => o.Id);

            tenantryConfiguration
                .Property<string>("_name")
                .HasColumnName("Name");

            tenantryConfiguration
                .Property<string>("_surname")
                .HasColumnName("Surname");

            tenantryConfiguration
                .Property<string>("_patronymic")
                .HasColumnName("Patronymic");

            tenantryConfiguration
                .Property<string>("FIO")
                .HasColumnName("FullName");

            tenantryConfiguration
                .Property<string>("Phone")
                .HasColumnName("PhoneNumber")
                .HasMaxLength(20);

            tenantryConfiguration
                .Property<string>("Address")
                .HasColumnName("Address");
        }
    }
}
