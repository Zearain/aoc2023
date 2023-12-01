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
    /// Gets or sets the ID of the Advent Day.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets the day number of the Advent Day.
    /// </summary>
    public int DayNumber { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the Advent Day has input.
    /// </summary>
    public bool HasInput { get; set; }

    /// <summary>
    /// Gets or sets the input for the Advent Day.
    /// </summary>
    public string? Input { get; set; }

    /// <summary>
    /// Gets or sets the solutions for the Advent Day.
    /// </summary>
    public IEnumerable<PartSolutionResponse> PartSolutions { get; set; } = Enumerable.Empty<PartSolutionResponse>();
}