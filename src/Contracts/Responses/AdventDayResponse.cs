// <copyright file="AdventDayResponse.cs" company="Zearain">
// Copyright (c) Zearain. All rights reserved.
// </copyright>

namespace Zearain.AoC23.Contracts.Responses;

/// <summary>
/// Represents an Advent Day.
/// </summary>
public sealed record AdventDayResponse
{
    /// <summary>
    /// Gets the ID of the Advent Day.
    /// </summary>
    public Guid Id { get; init; }

    /// <summary>
    /// Gets the day number of the Advent Day.
    /// </summary>
    public int DayNumber { get; init; }

    /// <summary>
    /// Gets a value indicating whether the Advent Day has input.
    /// </summary>
    public bool HasInput { get; init; }

    /// <summary>
    /// Gets the input for the Advent Day.
    /// </summary>
    public string? Input { get; init; }

    /// <summary>
    /// Gets the solutions for the Advent Day.
    /// </summary>
    public IEnumerable<PartSolutionResponse> PartSolutions { get; init; } = Enumerable.Empty<PartSolutionResponse>();
}
