// <copyright file="GondolaEngineSchematicReader.cs" company="Zearain">
// Copyright (c) Zearain. All rights reserved.
// </copyright>

using System.Globalization;

using Microsoft.Extensions.Logging;

using Zearain.AoC23.Domain.AdventDayAggregate.ValueObjects;

namespace Zearain.AoC23.Application.AdventDays.Services.GondolaEngineSchematics;

/// <summary>
/// Represents a reader for Gondola Engine Schematics.
/// </summary>
public sealed class GondolaEngineSchematicReader
{
    private readonly ILogger logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="GondolaEngineSchematicReader"/> class.
    /// </summary>
    /// <param name="logger">The logger to use.</param>
    public GondolaEngineSchematicReader(ILogger<GondolaEngineSchematicReader> logger)
    {
        this.logger = logger;
    }

    /// <summary>
    /// Reads the Gondola Engine Schematics from the given input.
    /// </summary>
    /// <param name="input">The input to read.</param>
    /// <returns>The Gondola Engine Schematics.</returns>
    public GondolaEngineSchematic Read(DayInput input)
    {
        var parts = new List<GondolaEngineSchematicPart>();
        var symbols = new List<GondolaEngineSchematicSymbol>();

        var lines = input.Lines.Select(l => l.TrimEnd('\r', '\n')).ToArray();
        var schematic = new char[lines.Length, lines[0].Length];
        for (var y = 0; y < lines.Length; y++)
        {
            var currentPartNumberDigits = new List<(char Digit, int X)>();
            for (var x = 0; x < lines[y].Length; x++)
            {
                schematic[y, x] = lines[y][x];

                if (char.IsNumber(lines[y][x]))
                {
                    currentPartNumberDigits.Add((lines[y][x], x));

                    if (x == lines[y].Length - 1)
                    {
                        var partNumber = int.Parse(string.Join(string.Empty, currentPartNumberDigits.Select(d => d.Digit)), CultureInfo.InvariantCulture);
                        var partNumberX = currentPartNumberDigits.Select(d => d.X).ToArray();
                        currentPartNumberDigits.Clear();

                        parts.Add(new GondolaEngineSchematicPart(partNumber, y, partNumberX));
                        this.logger.FoundPartNumber(partNumber, y, partNumberX);
                    }

                    continue;
                }

                if (currentPartNumberDigits.Any())
                {
                    var partNumber = int.Parse(string.Join(string.Empty, currentPartNumberDigits.Select(d => d.Digit)), CultureInfo.InvariantCulture);
                    var partNumberX = currentPartNumberDigits.Select(d => d.X).ToArray();
                    currentPartNumberDigits.Clear();

                    parts.Add(new GondolaEngineSchematicPart(partNumber, y, partNumberX));
                    this.logger.FoundPartNumber(partNumber, y, partNumberX);
                }

                if (lines[y][x] != '.')
                {
                    symbols.Add(new GondolaEngineSchematicSymbol(lines[y][x], y, x));
                    this.logger.FoundSymbol(lines[y][x], y, x);
                }
            }
        }

        return new GondolaEngineSchematic(schematic, parts.Where(p => symbols.Any(s => s.IsAdjacent(p))).ToArray(), symbols);
    }
}