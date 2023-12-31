// <copyright file="MapRange.cs" company="Zearain">
// Copyright (c) Zearain. All rights reserved.
// </copyright>

namespace Zearain.AoC23.Application.AdventDays.Services.SeedMapping;

/// <summary>
/// Represents a range of values.
/// </summary>
/// <param name="SourceStart">The starting value of the source range.</param>
/// <param name="DestinationStart">The starting value of the destination range.</param>
/// <param name="Length">The length of the ranges.</param>
public record MapRange(long SourceStart, long DestinationStart, long Length)
{
    /// <summary>
    /// Gets the adjustment number.
    /// </summary>
    public long Adjustment => this.DestinationStart - this.SourceStart;

    /// <summary>
    /// Checks if the given value is in range.
    /// </summary>
    /// <param name="value">The value to check.</param>
    /// <returns>True if the value is in range, false otherwise.</returns>
    public bool IsInRange(long value)
    {
        return value >= this.SourceStart && value <= this.SourceStart + this.Length;
    }
}