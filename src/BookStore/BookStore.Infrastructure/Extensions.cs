using BookStore.Infrastructure.DAL;
using BookStore.Infrastructure.QueryHandlers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace BookStore.Infrastructure;

public static class Extensions
{
    internal static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddQueries();
        services.AddDbContexts(configuration);
        return services;
    }

    public static IHostApplicationBuilder ConfigureInfrastructure(this IHostApplicationBuilder builder)
    {
        builder.Services.AddInfrastructure(builder.Configuration);
        builder.Logging.AddSeq();
        return builder;
    }
}
