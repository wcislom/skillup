using BookStore.Core;
using BookStore.Core.Repositories;
using BookStore.Infrastructure.DAL;

namespace BookStore.Infrastructure.Repositories;

internal class BookstoreRepository : IBookstoreRepository
{
    private BookstoreDbContext _dbContext;

    public BookstoreRepository(BookstoreDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Author?> GetAuthor(int authorId, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Authors.FindAsync(authorId, cancellationToken);
    }

    public async Task SaveChanges(CancellationToken cancellationToken = default)
    {
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}
