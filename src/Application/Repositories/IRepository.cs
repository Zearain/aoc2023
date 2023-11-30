// <copyright file="IRepository.cs" company="Zearain">
// Copyright (c) Zearain. All rights reserved.
// </copyright>

using ErrorOr;

using Zearain.AoC23.Application.Abstractions;
using Zearain.AoC23.Domain.Common;

namespace Zearain.AoC23.Application.Repositories;

/// <summary>
/// Represents a repository for a specific entity.
/// </summary>
/// <typeparam name="TEntity">The type of entity.</typeparam>
/// <typeparam name="TId">The type of the entity's identifier.</typeparam>
public interface IRepository<TEntity, TId>
    where TEntity : Entity<TId>
    where TId : notnull
{
    /// <summary>
    /// Gets the unit of work.
    /// </summary>
    IUnitOfWork UnitOfWork { get; }

    /// <summary>
    /// Gets the entity with the specified identifier.
    /// </summary>
    /// <param name="id">The identifier of the entity to get.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The entity with the specified identifier or an error.</returns>
    Task<ErrorOr<TEntity>> GetByIdAsync(TId id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets all entities.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>All entities.</returns>
    IAsyncEnumerable<TEntity> GetAllAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Adds the specified entity.
    /// </summary>
    /// <param name="entity">The entity to add.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The added entity.</returns>
    Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates the specified entity.
    /// </summary>
    /// <param name="entity">The entity to update.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The updated entity or an error.</returns>
    Task<ErrorOr<TEntity>> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes the entity with the specified identifier.
    /// </summary>
    /// <param name="id">The identifier of the entity to delete.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A value indicating whether the entity was deleted or an error.</returns>
    Task<ErrorOr<Deleted>> DeleteAsync(TId id, CancellationToken cancellationToken = default);
}