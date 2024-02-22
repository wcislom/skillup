using BookStore.Application.Core;
using BookStore.Application.Queries;
using BookStore.Application.Queries.DTO;
using BookStore.Core;
using BookStore.Infrastructure.DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace BookStore.Infrastructure.QueryHandlers;

public class GetBooksHandler(BookstoreDbContext dbContext, ILogger<GetBooksHandler> logger) : IQueryHandler<GetBooks, IEnumerable<BookDto>>
{
    private readonly BookstoreDbContext _dbContext = dbContext;
    private readonly ILogger<GetBooksHandler> _logger = logger;

    public async Task<IEnumerable<BookDto>> HandleAsync(GetBooks query)
    {
        _logger.LogInformation("Getting list of books with authors");

        var sw = Stopwatch.StartNew();
        var books = (await _dbContext.Books
           .AsNoTracking()
           .Include(b => b.Author)
           .ToListAsync())
           .Select(b => b.AsDto()); 
        sw.Stop();

        AccessChangeTracker();
        _logger.LogDebug("GetBooksHandler took {ElapsedMilliseconds}ms", sw.ElapsedMilliseconds);

        return books;
    }

    private void LoadReferencedData()
    {
        var book = _dbContext.Books.Find(1)!;
        _dbContext.Entry(book).Reference(b => b.Author).Load();
    }

    private void LoadCollectionData()
    {
        var author = _dbContext.Authors.Find(1)!;
        _dbContext.Entry(author).Collection(a => a.Books).Load();
    }

    private void AccessChangeTracker()
    {
        var book = _dbContext.Books.Find(1)!;
        book.UpdatePrice(99.99M);
        _dbContext.ChangeTracker.DetectChanges();
        var changeTrackerShortView = _dbContext.ChangeTracker.DebugView.ShortView;

        var author = _dbContext.Authors.Find(1)!;
        author.Books.RemoveAt(0);
        _dbContext.ChangeTracker.DetectChanges();
        var changeTrackerLongView = _dbContext.ChangeTracker.DebugView.LongView;
    }

    private void DeleteRelatedData()
    {
        // option 1
        var book = _dbContext.Books.Find(1)!;
        _dbContext.Remove(book);

        // option 2
        var author = _dbContext.Authors.Find(1)!;
        author.Books.RemoveAt(0);

        // option 3
        _dbContext.Entry(book).State = EntityState.Deleted;

        // option 4
        _dbContext.Books.Where(b => b.Id == 1).ExecuteDelete();
    }

    private void TransactionExample()
    {
        using var transaction = _dbContext.Database.BeginTransaction();
        try
        {
            _dbContext.Books.Add(new Book("New book", new DateOnly(2022, 1, 1), 99.99M));
            _dbContext.Authors
                .Where(a => a.Id == 1)
                .ExecuteUpdate(setter => setter.SetProperty(p => p.Name.FirstName, "Carl")); // this is executed immediately
            _dbContext.SaveChanges();
            transaction.Commit();
        }
        catch (Exception)
        {
            transaction.Rollback();
        }
    }
}


