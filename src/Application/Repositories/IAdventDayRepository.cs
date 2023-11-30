// <copyright file="IAdventDayRepository.cs" company="Zearain">
// Copyright (c) Zearain. All rights reserved.
// </copyright>

using ErrorOr;

using Zearain.AoC23.Domain.AdventDayAggregate;
using Zearain.AoC23.Domain.AdventDayAggregate.ValueObjects;

namespace Zearain.AoC23.Application.Repositories;

/// <summary>
/// Represents a repository for <see cref="AdventDay"/> entities.
/// </summary>
public interface IAdventDayRepository : IRepository<AdventDay, AdventDayId>
{
    /// <summary>
    /// Gets the <see cref="AdventDay"/> with the specified day number.
    /// </summary>
    /// <param name="dayNumber">The day number of the <see cref="AdventDay"/> to get.</param>
    /// <returns>The <see cref="AdventDay"/> with the specified day number or an error.</returns>
    Task<ErrorOr<AdventDay>> GetByDayNumberAsync(int dayNumber);
}