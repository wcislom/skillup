using Shared.Core.Interfaces;

namespace BookStore.Core.Repositories;

public interface IBookstoreRepository : IRepository
{
    Task<Author?> GetAuthor(int authorId, CancellationToken cancellationToken);
    Task SaveChanges(CancellationToken cancellationToken);
}
