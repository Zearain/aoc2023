// <copyright file="AoC23DbContext.cs" company="Zearain">
// Copyright (c) Zearain. All rights reserved.
// </copyright>

using Microsoft.EntityFrameworkCore;

using Zearain.AoC23.Application.Abstractions;
using Zearain.AoC23.Domain.AdventDayAggregate;
using Zearain.AoC23.Domain.Common;
using Zearain.AoC23.Infrastructure.Persistence.Inteceptors;

namespace Zearain.AoC23.Infrastructure.Persistence;

/// <summary>
/// The database context for the Advent of Code 2023 application.
/// </summary>
public class AoC23DbContext : DbContext, IUnitOfWork
{
    private readonly PublishDomainEventsInterceptor publishDomainEventsInterceptor;

    /// <summary>
    /// Initializes a new instance of the <see cref="AoC23DbContext"/> class.
    /// </summary>
    /// <param name="options">The database context options.</param>
    /// <param name="publishDomainEventsInterceptor">The publish domain events interceptor.</param>
    public AoC23DbContext(DbContextOptions<AoC23DbContext> options, PublishDomainEventsInterceptor publishDomainEventsInterceptor)
        : base(options)
    {
        this.publishDomainEventsInterceptor = publishDomainEventsInterceptor;
    }

    /// <summary>
    /// Gets or sets the <see cref="DbSet{TEntity}"/> of <see cref="AdventDay"/>s.
    /// </summary>
    public DbSet<AdventDay> AdventDays { get; set; } = null!;

    /// <inheritdoc/>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .Ignore<List<IDomainEvent>>()
            .ApplyConfigurationsFromAssembly(typeof(AoC23DbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }

    /// <inheritdoc/>
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.AddInterceptors(this.publishDomainEventsInterceptor);
        base.OnConfiguring(optionsBuilder);
    }
}