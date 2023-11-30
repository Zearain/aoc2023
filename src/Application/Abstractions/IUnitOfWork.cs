// <copyright file="IUnitOfWork.cs" company="Zearain">
// Copyright (c) Zearain. All rights reserved.
// </copyright>

namespace Zearain.AoC23.Application.Abstractions;

/// <summary>
/// Represents a unit of work.
/// </summary>
public interface IUnitOfWork
{
    /// <summary>
    /// Saves the changes made in this unit of work.
    /// </summary>
    /// <param name="acceptAllChangesOnSuccess">Indicates whether all changes should be accepted on success.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task that represents the asynchronous save operation. The task result contains the number of state entries written to the database.</returns>
    Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default);

    /// <summary>
    /// Saves the changes made in this unit of work.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task that represents the asynchronous save operation. The task result contains the number of state entries written to the database.</returns>
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}