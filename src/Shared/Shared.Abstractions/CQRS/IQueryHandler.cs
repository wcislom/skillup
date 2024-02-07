using Shared.Abstractions.CQRS;

namespace BookStore.Application.Core;

public interface IQueryHandler<TQuery, TResult> where TQuery: IQuery
                                                where TResult : class
{
    Task<TResult> HandleAsync(TQuery query);
}
