// <copyright file="AoC23DbContext.cs" company="Zearain">
// Copyright (c) Zearain. All rights reserved.
// </copyright>

using Microsoft.EntityFrameworkCore;

using Zearain.AoC23.Application.Abstractions;
using Zearain.AoC23.Domain.AdventDayAggregate;

namespace Zearain.AoC23.Infrastructure.Persistence;

/// <summary>
/// The database context for the Advent of Code 2023 application.
/// </summary>
public class AoC23DbContext : DbContext, IUnitOfWork
{
    /// <summary>
    /// Initializes a new instance of the <see cref="AoC23DbContext"/> class.
    /// </summary>
    /// <param name="options">The database context options.</param>
    public AoC23DbContext(DbContextOptions<AoC23DbContext> options)
        : base(options)
    {
    }

    /// <summary>
    /// Gets or sets the <see cref="DbSet{TEntity}"/> of <see cref="AdventDay"/>s.
    /// </summary>
    public DbSet<AdventDay> AdventDays { get; set; } = null!;

    /// <inheritdoc/>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AoC23DbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}