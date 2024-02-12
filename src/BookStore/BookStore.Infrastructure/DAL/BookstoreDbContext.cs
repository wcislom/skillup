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

    public DbSet<Author> Authors { get; internal set; }

    override protected void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(BookstoreDbContext).Assembly);
    }

    public void AddData()
    {

        var a = new Author("John", "Smith");
        a.AddBook(new Book("1984", DateOnly.Parse("2020-12-01"), 19.99M));
        Add(a);
        SaveChanges();
    }
}
