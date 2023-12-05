// <copyright file="SeedAlmanacReader.cs" company="Zearain">
// Copyright (c) Zearain. All rights reserved.
// </copyright>

using System.Globalization;

using Microsoft.Extensions.Logging;

using Zearain.AoC23.Domain.AdventDayAggregate.ValueObjects;

namespace Zearain.AoC23.Application.AdventDays.Services.SeedMapping;

/// <summary>
/// Represents a reader for the seed almanac input.
/// </summary>
public class SeedAlmanacReader
{
    private readonly ILogger<SeedAlmanacReader> logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="SeedAlmanacReader"/> class.
    /// </summary>
    /// <param name="logger">The logger.</param>
    public SeedAlmanacReader(ILogger<SeedAlmanacReader> logger)
    {
        this.logger = logger;
    }

    /// <summary>
    /// Gets the lowest mapped value for the given almanac.
    /// </summary>
    /// <param name="almanac">The almanac to get the lowest mapped value for.</param>
    /// <returns>The lowest mapped value.</returns>
    public static int GetLowestMappedValue(SeedAlmanac almanac)
    {
        var mappings = almanac.GetSeedValueMappings();
        var lowestMappedValue = mappings.Select(x => x[^1].Value).Min();
        return lowestMappedValue;
    }

    /// <summary>
    /// Reads the seed almanac from the given input.
    /// </summary>
    /// <param name="input">The input to read from.</param>
    /// <returns>The seed almanac.</returns>
#pragma warning disable SA1010 // Opening square brackets should be spaced correctly
    public SeedAlmanac Read(DayInput input)
    {
        var sectionLines = input.SectionLines;

        var seedType = sectionLines[0][0].Split(':')[0].Trim().TrimEnd('s');
        var seedValues = sectionLines[0][0].Split(':')[1].Trim().Split(' ').Where(s => !string.IsNullOrWhiteSpace(s)).Select(int.Parse).ToArray();

        var maps = new List<AlmanacMap>();
        for (int i = 1; i < sectionLines.Length; i++)
        {
            maps.Add(this.ReadMap(sectionLines[i]));
        }

        return new SeedAlmanac(seedType, seedValues, [.. maps]);
    }

    /// <summary>
    /// Reads a map from the given lines.
    /// </summary>
    /// <param name="lines">The lines to read from.</param>
    /// <returns>The map.</returns>
    public AlmanacMap ReadMap(string[] lines)
    {
        var splitHeader = lines[0].Split("-to-").Select(s => s.Trim()).ToArray();
        var from = splitHeader[0];
        var to = splitHeader[1].Split(' ')[0];

        var ranges = new List<MapRange>();
        for (int i = 1; i < lines.Length; i++)
        {
            var splitLine = lines[i].Split(' ').Select(s => s.Trim()).ToArray();
            var destinationStart = int.Parse(splitLine[0], CultureInfo.InvariantCulture);
            var sourceStart = int.Parse(splitLine[1], CultureInfo.InvariantCulture);
            var length = int.Parse(splitLine[2], CultureInfo.InvariantCulture);
            ranges.Add(new MapRange(sourceStart, destinationStart, length));
        }

        return new AlmanacMap(from, to, [.. ranges]);
#pragma warning restore SA1010 // Opening square brackets should be spaced correctly
    }
}