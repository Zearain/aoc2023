// <copyright file="AlmanacMap.cs" company="Zearain">
// Copyright (c) Zearain. All rights reserved.
// </copyright>

namespace Zearain.AoC23.Application.AdventDays.Services.SeedMapping;

/// <summary>
/// Represents a map of the seed almanac.
/// </summary>
/// <param name="From">The type that this maps from.</param>
/// <param name="To">The type that this maps to.</param>
/// <param name="Ranges">The ranges of values that this maps.</param>
public record AlmanacMap(string From, string To, MapRange[] Ranges)
{
    /// <summary>
    /// Gets the mapped value for the given value.
    /// </summary>
    /// <param name="value">The value to map.</param>
    /// <returns>The mapped value.</returns>
    public long GetMappedValue(long value)
    {
        var mappedRange = this.Ranges.FirstOrDefault(r => r.IsInRange(value));
        if (mappedRange is null)
        {
            return value;
        }

        return value - mappedRange.SourceStart + mappedRange.DestinationStart;
    }

    /// <summary>
    /// Checks if the given value is in range.
    /// </summary>
    /// <param name="value">The value to check.</param>
    /// <returns>True if the value is in range, false otherwise.</returns>
    public bool IsInRange(long value)
    {
        return this.Ranges.Any(r => r.IsInRange(value));
    }
}