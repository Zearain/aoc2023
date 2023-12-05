// <copyright file="SeedAlmanac.cs" company="Zearain">
// Copyright (c) Zearain. All rights reserved.
// </copyright>

using System.Collections.Concurrent;

namespace Zearain.AoC23.Application.AdventDays.Services.SeedMapping;

/// <summary>
/// Represents the seed almanac.
/// </summary>
/// <param name="SeedType">The type of the seed.</param>
/// <param name="SeedValues">The seed values.</param>
/// <param name="Maps">The maps of the seed almanac.</param>
public record SeedAlmanac(string SeedType, long[] SeedValues, AlmanacMap[] Maps)
{
    /// <summary>
    /// Gets the seed value mappings.
    /// </summary>
    /// <returns>The seed value mappings.</returns>
    public MappingResult[][] GetSeedValueMappings()
    {
        var mappings = new List<MappingResult[]>();
        foreach (var seedValue in this.SeedValues)
        {
            mappings.Add(this.GetMappedValuesFor(this.SeedType, seedValue));
        }

#pragma warning disable SA1010 // Opening square brackets should be spaced correctly
        return [.. mappings];
    }

    /// <summary>
    /// Gets the lowest final mapped value for the seed ranges.
    /// </summary>
    /// <returns>The lowest final mapped value.</returns>
    public long GetLowestFinalMappedValueForSeedRanges()
    {
        var seedValueRanges = this.GetSeedValueRanges().Select(r => (From: r[0], To: r[0] + r[1])).ToList();

        foreach (var map in this.Maps)
        {
            var orderedMapRanges = map.Ranges.OrderBy(r => r.SourceStart).ToArray();

            var newRanges = new List<(long From, long To)>();
            foreach (var seedValueRange in seedValueRanges)
            {
                var currentRange = seedValueRange;
                foreach (var mapRange in orderedMapRanges)
                {
                    if (currentRange.From < mapRange.SourceStart)
                    {
                        newRanges.Add((currentRange.From, new[] { currentRange.To, mapRange.SourceStart}.Min()));
                        currentRange.From = mapRange.SourceStart;
                        if (currentRange.From > currentRange.To)
                        {
                            break;
                        }
                    }

                    if (currentRange.From <= mapRange.SourceStart + mapRange.Length)
                    {
                        newRanges.Add((currentRange.From + mapRange.Adjustment, new[] { currentRange.To, mapRange.SourceStart + mapRange.Length }.Min() + mapRange.Adjustment));
                        currentRange.From = mapRange.DestinationStart + mapRange.Length;
                        if (currentRange.From > currentRange.To)
                        {
                            break;
                        }
                    }
                }

                if (currentRange.From <= currentRange.To)
                {
                    newRanges.Add(currentRange);
                }
            }

            seedValueRanges = newRanges;
        }

        return seedValueRanges.Select(r => r.From).Min();
    }

    /// <summary>
    /// Gets the mapped values for the given type and value.
    /// </summary>
    /// <param name="from">The type to map from.</param>
    /// <param name="value">The value to map.</param>
    /// <returns>The mapped values.</returns>
    public MappingResult[] GetMappedValuesFor(string from, long value)
    {
        var mappedValues = new HashSet<MappingResult> { new(from, value) };
        var map = this.Maps.FirstOrDefault(m => m.From == from);
        if (map is null)
        {
            return [.. mappedValues];
        }

        var recursiveMapping = this.GetMappedValuesFor(map.To, map.GetMappedValue(value));
        foreach (var mapping in recursiveMapping)
        {
            mappedValues.Add(mapping);
        }

        return [.. mappedValues];
#pragma warning restore SA1010 // Opening square brackets should be spaced correctly
    }

    private List<long[]> GetSeedValueRanges()
    {
        var result = new List<long[]>();
        for (int i = 0; i < this.SeedValues.Length; i += 2)
        {
            if (i + 1 < this.SeedValues.Length)
            {
                result.Add(new long[] { this.SeedValues[i], this.SeedValues[i + 1] });
            }
            else
            {
                result.Add(new long[] { this.SeedValues[i] });
            }
        }

        return result;
    }
}