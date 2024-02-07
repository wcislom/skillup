using BookStore.Application.Commands;
using BookStore.Application.Commands.Handlers;
using Microsoft.Extensions.DependencyInjection;
using Shared.Abstractions.CQRS;

namespace BookStore.Application;

public static class Extensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<ICommandHandler<CreateBook>, CreateBookHandler>();
        return services;
    }
}
