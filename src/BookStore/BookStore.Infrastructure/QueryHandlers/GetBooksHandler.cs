using BookStore.Application.Core;
using BookStore.Application.Queries;
using BookStore.Application.Queries.DTO;
using BookStore.Infrastructure.DAL;
using BookStore.Infrastructure.DAL.Mappings;
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
        var books = await _dbContext.Books
           .AsNoTracking()
           .Include(b => b.Author)
           .Select(b => new { b.Title, b.BasePrice, AuthorName = $"{b.Author.FirstName} {b.Author.LastName}" }) // Less data to transfer
           .ToListAsync();
        sw.Stop();
        _logger.LogDebug("GetBooksHandler took {ElapsedMilliseconds}ms", sw.ElapsedMilliseconds);

        return books.Select(b => b.AsDto());
    }
}
