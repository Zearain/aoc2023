// <copyright file="DependencyInjection.cs" company="Zearain">
// Copyright (c) Zearain. All rights reserved.
// </copyright>

using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Zearain.AoC23.Application;

/// <summary>
/// Contains the dependency injection configuration for the application layer.
/// </summary>
public static class DependencyInjection
{
    /// <summary>
    /// Adds the application layer dependencies to the service collection.
    /// </summary>
    /// <param name="services">The service collection.</param>
    /// <returns>The service collection with the application layer dependencies added.</returns>
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(configuration =>
        {
            configuration.RegisterServicesFromAssemblies(typeof(DependencyInjection).Assembly, typeof(Domain.Common.IDomainEvent).Assembly);
        });

        // services.AddValidatorsFromAssemblyContaining<DependencyInjection>();
        return services;
    }
}