// <copyright file="SeedAlmanac.cs" company="Zearain">
// Copyright (c) Zearain. All rights reserved.
// </copyright>

namespace Zearain.AoC23.Application.AdventDays.Services.SeedMapping;

/// <summary>
/// Represents the seed almanac.
/// </summary>
/// <param name="SeedType">The type of the seed.</param>
/// <param name="SeedValues">The seed values.</param>
/// <param name="Maps">The maps of the seed almanac.</param>
public record SeedAlmanac(string SeedType, int[] SeedValues, AlmanacMap[] Maps)
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
    /// Gets the mapped values for the given type and value.
    /// </summary>
    /// <param name="from">The type to map from.</param>
    /// <param name="value">The value to map.</param>
    /// <returns>The mapped values.</returns>
    public MappingResult[] GetMappedValuesFor(string from, int value)
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
}