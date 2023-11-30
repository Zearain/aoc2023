// <copyright file="DependencyInjection.cs" company="Zearain">
// Copyright (c) Zearain. All rights reserved.
// </copyright>

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using Zearain.AoC23.Application.Abstractions;
using Zearain.AoC23.Application.Repositories;
using Zearain.AoC23.Infrastructure.Persistence;
using Zearain.AoC23.Infrastructure.Persistence.Inteceptors;
using Zearain.AoC23.Infrastructure.Persistence.Repositories;
using Zearain.AoC23.Infrastructure.Services;

namespace Zearain.AoC23.Infrastructure;

/// <summary>
/// Contains the dependency injection configuration for the application layer.
/// </summary>
public static class DependencyInjection
{
    /// <summary>
    /// Adds the infrastructure layer dependencies to the service collection.
    /// </summary>
    /// <param name="services">The service collection.</param>
    /// <param name="configuration">The configuration to use.</param>
    /// <returns>The service collection with the infrastructure layer dependencies added.</returns>
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddPersistence(configuration);

        services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
        return services;
    }

    /// <summary>
    /// Adds the persistence layer dependencies to the service collection.
    /// </summary>
    /// <param name="services">The service collection.</param>
    /// <param name="configuration">The configuration to use.</param>
    /// <returns>The service collection with the persistence layer dependencies added.</returns>
    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AoC23DbContext>(builder =>
        {
            builder.UseNpgsql(configuration.GetConnectionString("DefaultConnection"));
        });
        services.AddScoped<PublishDomainEventsInterceptor>();
        services.AddScoped<IAdventDayRepository, AdventDayRepository>();

        return services;
    }
}