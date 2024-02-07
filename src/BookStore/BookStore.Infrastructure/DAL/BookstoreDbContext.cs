using BookStore.Core;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Infrastructure.DAL;

public class BookstoreDbContext : DbContext
{
    public BookstoreDbContext(DbContextOptions<BookstoreDbContext> options)
        : base(options)
    {

    }

    public DbSet<Book> Books { get; internal set; }

    override protected void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(BookstoreDbContext).Assembly);
    }
}
