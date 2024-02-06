﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BookStore.Infrastructure.DAL;

internal static class Extensions
{
    internal static IServiceCollection AddDbContexts(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<BookstoreDbContext>(optionsBuilder =>
        {
            optionsBuilder.EnableSensitiveDataLogging()
            .LogTo(Console.WriteLine)
            .UseSqlServer(configuration.GetConnectionString(nameof(BookstoreDbContext)));
        });
        return services;
    }
}
