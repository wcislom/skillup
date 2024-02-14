using BookStore.Application.Core;
using BookStore.Infrastructure.DAL;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Shared.Abstractions.CQRS;
using Shared.Core.Interfaces;

namespace BookStore.Infrastructure;

public static class Extensions
{
    internal static IServiceCollection AddQueries(this IServiceCollection services)
    {
        services.Scan(s => s.FromCallingAssembly()
                            .AddClasses(c => c.AssignableTo(typeof(IQueryHandler<,>)))
                            .AsImplementedInterfaces()
                            .WithTransientLifetime());
        return services;
    }

    internal static IServiceCollection AddCommands(this IServiceCollection services)
    {
        services.Scan(s => s.FromCallingAssembly()
                            .AddClasses(c => c.AssignableTo(typeof(ICommandHandler<>)))
                            .AsImplementedInterfaces()
                            .WithTransientLifetime());
        return services;
    }

    internal static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddQueries();
        services.AddCommands();
        services.Scan(s => s.FromCallingAssembly()
                                   .AddClasses(c => c.AssignableTo<IRepository>()
                                   , false)
                                   .AsImplementedInterfaces()
                                   .WithScopedLifetime());

        services.AddDbContexts(configuration);
        return services;
    }

    public static IHostApplicationBuilder ConfigureInfrastructure(this IHostApplicationBuilder builder)
    {
        builder.Services.AddInfrastructure(builder.Configuration);
        return builder;
    }
}


