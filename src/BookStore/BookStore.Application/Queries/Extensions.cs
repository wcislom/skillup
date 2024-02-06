using BookStore.Application.Core;
using BookStore.Application.Queries.DTO;
using BookStore.Application.Queries.Handlers;
using Microsoft.Extensions.DependencyInjection;

namespace BookStore.Application.Queries
{
    internal static class Extensions
    {
        public static IServiceCollection AddQueries(this IServiceCollection services)
        {
            services.AddTransient<IQueryHandler<GetBooks, IEnumerable<BookDto>>, GetBooksHandler>();
            return services;
        }
    }
}
