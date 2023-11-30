// <copyright file="AdventDayResponse.cs" company="Zearain">
// Copyright (c) Zearain. All rights reserved.
// </copyright>

namespace Zearain.AoC23.Contracts.Responses;

public sealed record PartSolutionResponse
{
    /// <summary>
    /// Gets the part number of the Part Solution.
    /// </summary>
    public int PartNumber { get; init; }

    /// <summary>
    /// Gets the solution for the Part Solution.
    /// </summary>
    public string Solution { get; init; } = string.Empty;
}