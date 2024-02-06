using BookStore.Application.Queries;
using Microsoft.Extensions.DependencyInjection;

namespace BookStore.Application
{
    public static class Extensions
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddQueries();
            return services;
        }
    }
}
