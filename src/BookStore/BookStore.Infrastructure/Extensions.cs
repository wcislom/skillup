using BookStore.Infrastructure.DAL;
using BookStore.Infrastructure.QueryHandlers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BookStore.Infrastructure;

public static class Extensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddQueries();
        services.AddDbContexts(configuration);
        return services;
    }
}
