using Microsoft.EntityFrameworkCore;
using RentingOutBooksService.Application.Common.Interfaces;
using RentingOutBooksService.Domain.Aggregates.RentAggregate;
using RentingOutBooksService.Infrastructure.Configurations;

namespace RentingOutBooksService.Infrastructure
{
    public class RentMicroServiceContext
        : DbContext, IRentMicroServiceContext
    {
        protected readonly string _defaultSchema = "LIBRARY_RENT_BOOKS";

        public RentMicroServiceContext(DbContextOptions<RentMicroServiceContext> options)
            : base(options)
        {
        }

        public DbSet<Rent> Rents { get; set; }
        public DbSet<RentStatus> RentStatuses { get; set; }
        public DbSet<Tenantry> Tenantries { get; set; }

        public void Migrate()
        {
            Database.Migrate();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new RentConfiguration());
            modelBuilder.ApplyConfiguration(new RentStatusConfiguration());
            modelBuilder.ApplyConfiguration(new TenantryConfiguration());

            modelBuilder.HasDefaultSchema(_defaultSchema);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(RentMicroServiceContext).Assembly);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Server=localhost;User Id=postgres;Password=0000;Port=5432;Database=LibraryServiceDB;");
        }

        protected static DbContextOptions<T> ChangeOptionsType<T>(DbContextOptions options) where T : DbContext
        {
            return new DbContextOptionsBuilder<T>()
                .Options;
        }
    }
}
