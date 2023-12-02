// <copyright file="DependencyInjection.cs" company="Zearain">
// Copyright (c) Zearain. All rights reserved.
// </copyright>

using System.Reflection;

using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

using Zearain.AoC23.Application.AdventDays.Services;

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
    /// <param name="additionalAssemblies">Additional assemblies to scan for MediatR handlers.</param>
    /// <returns>The service collection with the application layer dependencies added.</returns>
    public static IServiceCollection AddApplication(this IServiceCollection services, params Assembly[]? additionalAssemblies)
    {
        services.AddMediatR(configuration =>
        {
            configuration.RegisterServicesFromAssemblies(typeof(DependencyInjection).Assembly, typeof(Domain.Common.IDomainEvent).Assembly);
            if (additionalAssemblies != null)
            {
                configuration.RegisterServicesFromAssemblies(additionalAssemblies);
            }
        });

        services.AddScoped<ColoredCubeGameService>();

        // services.AddValidatorsFromAssemblyContaining<DependencyInjection>();
        return services;
    }
}