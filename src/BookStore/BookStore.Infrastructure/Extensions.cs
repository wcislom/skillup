using BookStore.Infrastructure.QueryHandlers;
using Microsoft.Extensions.DependencyInjection;

namespace BookStore.Infrastructure
{
    public static class Extensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddQueries();
            return services;
        }
    }
}
