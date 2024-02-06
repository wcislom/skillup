using BookStore.Application.Core;
using BookStore.Application.Queries;
using BookStore.Application.Queries.DTO;
using Microsoft.Extensions.DependencyInjection;

namespace BookStore.Infrastructure.QueryHandlers;

internal static class Extensions
{
    public static IServiceCollection AddQueries(this IServiceCollection services)
    {
        services.AddTransient<IQueryHandler<GetBooks, IEnumerable<BookDto>>, GetBooksHandler>();
        return services;
    }
}
