// <copyright file="PartSolutionResponse.cs" company="Zearain">
// Copyright (c) Zearain. All rights reserved.
// </copyright>

namespace Zearain.AoC23.Contracts.Responses;

/// <summary>
/// Represents a Part Solution.
/// </summary>
public sealed record PartSolutionResponse
{
    /// <summary>
    /// Gets or sets the part number of the Part Solution.
    /// </summary>
    public int PartNumber { get; set; }

    /// <summary>
    /// Gets or sets the solution for the Part Solution.
    /// </summary>
    public string Solution { get; set; } = string.Empty;
}