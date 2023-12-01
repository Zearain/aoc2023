// <copyright file="IAdventDayHub.cs" company="Zearain">
// Copyright (c) Zearain. All rights reserved.
// </copyright>

using Zearain.AoC23.Contracts.Responses;

namespace Zearain.AoC23.Contracts.Events;

/// <summary>
/// Represents the methods available on the Advent Day Hub.
/// </summary>
public interface IAdventDayHub
{
    /// <summary>
    /// Notifies the client that an Advent Day has been created.
    /// </summary>
    /// <param name="adventDay">The Advent Day that was created.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    Task AdventDayCreated(AdventDayResponse adventDay);

    /// <summary>
    /// Notifies the client that an advent day has received input.
    /// </summary>
    /// <param name="adventDayId">The ID of the Advent Day.</param>
    /// <param name="addedInput">The input that was added.</param>
    /// <param name="hasInput">A value indicating whether the input is valid.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    Task DayInputAdded(Guid adventDayId, string addedInput, bool hasInput);

    /// <summary>
    /// Notifies the client that a part solution has been added.
    /// </summary>
    /// <param name="adventDayId">The ID of the Advent Day.</param>
    /// <param name="partSolution">The part solution that was added.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    Task PartSolutionAdded(Guid adventDayId, PartSolutionResponse partSolution);
}