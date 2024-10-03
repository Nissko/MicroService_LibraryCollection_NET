using LibraryCollection.Domain.Aggregates.BookAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace LibraryCollection.Application.Common.Interfaces
{
    public interface IBooksContext
    {
        DatabaseFacade Database { get; }

        public DbSet<Book> Books { get; set; }
        public DbSet<BookCategory> Categories { get; set; }
        public DbSet<BookGenre> Genres { get; set; }
        public DbSet<BookQuote> Quotes { get; set; }
        public DbSet<BookStatus> Statuses { get; set; }

        void Migrate();

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
