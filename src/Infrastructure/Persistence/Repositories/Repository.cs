// <copyright file="Repository.cs" company="Zearain">
// Copyright (c) Zearain. All rights reserved.
// </copyright>

using ErrorOr;
using Microsoft.EntityFrameworkCore;

using Zearain.AoC23.Application.Abstractions;
using Zearain.AoC23.Application.Repositories;
using Zearain.AoC23.Domain.Common;

namespace Zearain.AoC23.Infrastructure;

/// <summary>
/// Represents a base repository for <see cref="Entity{TId}"/> entities.
/// </summary>
/// <typeparam name="TEntity">The type of entity.</typeparam>
/// <typeparam name="TId">The type of the entity's identifier.</typeparam>
public abstract class Repository<TEntity, TId> : IRepository<TEntity, TId>
    where TEntity : Entity<TId>
    where TId : notnull
{
#pragma warning disable CA1051 // Do not declare visible instance fields
#pragma warning disable SA1401 // Fields should be private
    /// <summary>
    /// The database context.
    /// </summary>
    protected DbContext dbContext;

    /// <summary>
    /// The database set that contains the entities.
    /// </summary>
    protected DbSet<TEntity> dbSet;
#pragma warning restore CA1051 // Do not declare visible instance fields
#pragma warning restore SA1401 // Fields should be private

    /// <summary>
    /// Initializes a new instance of the <see cref="Repository{TEntity, TId}"/> class.
    /// </summary>
    /// <param name="dbContext">The database context.</param>
    protected Repository(DbContext dbContext)
    {
        this.dbContext = dbContext;
        this.dbSet = dbContext.Set<TEntity>();
    }

    /// <inheritdoc/>
    public abstract IUnitOfWork UnitOfWork { get; }

    /// <inheritdoc/>
    public virtual IAsyncEnumerable<TEntity> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return this.dbSet.AsAsyncEnumerable();
    }

    /// <inheritdoc/>
    public virtual async Task<ErrorOr<Deleted>> DeleteAsync(TId id, CancellationToken cancellationToken = default)
    {
        TEntity? entityToDelete = await this.dbSet.FindAsync(new object[] { id }, cancellationToken);

        if (entityToDelete is null)
        {
            return Error.NotFound();
        }

        if (this.dbContext.Entry(entityToDelete).State == EntityState.Detached)
        {
            this.dbSet.Attach(entityToDelete);
        }

        this.dbSet.Remove(entityToDelete);

        return Result.Deleted;
    }

    /// <inheritdoc/>
    public virtual async Task<ErrorOr<TEntity>> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        await Task.CompletedTask;

        this.dbSet.Update(entity);

        return entity;
    }

    /// <inheritdoc/>
    public virtual async Task<ErrorOr<TEntity>> GetByIdAsync(TId id, CancellationToken cancellationToken = default)
    {
        var entity = await this.dbSet.FindAsync(new object[] { id }, cancellationToken);
        return entity is null ? Error.NotFound() : entity;
    }

    /// <inheritdoc/>
    public virtual async Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        var entityEntry = await this.dbSet.AddAsync(entity, cancellationToken);
        return entityEntry.Entity;
    }
}