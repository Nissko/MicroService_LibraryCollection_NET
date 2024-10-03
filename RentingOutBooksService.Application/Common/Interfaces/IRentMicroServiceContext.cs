using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using RentingOutBooksService.Domain.Aggregates.RentAggregate;

namespace RentingOutBooksService.Application.Common.Interfaces
{
    public interface IRentMicroServiceContext
    {
        DatabaseFacade Database { get; }

        public DbSet<Rent> Rents { get; set; }
        public DbSet<RentStatus> RentStatuses { get; set; }
        public DbSet<Tenantry> Tenantries { get; set; }

        void Migrate();

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
