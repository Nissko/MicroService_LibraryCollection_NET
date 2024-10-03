using LibraryCollection.Application.Common.Interfaces;
using LibraryCollection.Domain.Aggregates.BookAggregate;
using LibraryCollection.Infrastructure.Configurations;
using Microsoft.EntityFrameworkCore;

namespace LibraryCollection.Infrastructure
{
    public class BooksContext
        : DbContext, IBooksContext
    {
        protected readonly string _defaultSchema = "LIBRARY_BOOKS";

        public BooksContext(DbContextOptions<BooksContext> options)
            : base(options)
        {
        }

        public DbSet<Book> Books { get; set; }
        public DbSet<BookCategory> Categories { get; set; }
        public DbSet<BookGenre> Genres { get; set; }
        public DbSet<BookQuote> Quotes { get; set; }
        public DbSet<BookStatus> Statuses { get; set; }

        public void Migrate()
        {
            Database.Migrate();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new BookConfiguration());
            modelBuilder.ApplyConfiguration(new CategoryConfiguration());
            modelBuilder.ApplyConfiguration(new GenreConfiguration());
            modelBuilder.ApplyConfiguration(new QuoteConfiguration());
            modelBuilder.ApplyConfiguration(new StatusConfiguration());

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(BooksContext).Assembly);
        }

        public BooksContext()
        {
            Database.EnsureCreated();
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
