// <copyright file="GondolaEngineSchematicReader.cs" company="Zearain">
// Copyright (c) Zearain. All rights reserved.
// </copyright>

using System.Globalization;

using Microsoft.Extensions.Logging;

using Zearain.AoC23.Domain.AdventDayAggregate.ValueObjects;

namespace Zearain.AoC23.Application.AdventDays.Services;

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

                if (char.IsDigit(lines[y][x]))
                {
                    currentPartNumberDigits.Add((lines[y][x], x));
                }
                else if (currentPartNumberDigits.Any())
                {
                    var partNumber = int.Parse(string.Join(string.Empty, currentPartNumberDigits.Select(d => d.Digit)), CultureInfo.InvariantCulture);
                    var partNumberX = currentPartNumberDigits.Select(d => d.X).ToArray();
                    currentPartNumberDigits.Clear();
                    parts.Add(new GondolaEngineSchematicPart(partNumber, y, partNumberX));
                    this.logger.LogTrace("Found part number {PartNumber} at {Y},{X}", partNumber, y, partNumberX);
                }

                if (!char.IsDigit(lines[y][x]) && lines[y][x] != '.')
                {
                    this.logger.LogTrace("Found symbol {Symbol} at {Y},{X}", lines[y][x], y, x);
                    symbols.Add(new GondolaEngineSchematicSymbol(lines[y][x], y, x));
                }
            }
        }

        return new GondolaEngineSchematic(schematic, parts.Where(p => symbols.Any(s => IsAdjacent(p, s))).ToArray(), symbols);
    }

    private static bool IsAdjacent(GondolaEngineSchematicPart partNumber, GondolaEngineSchematicSymbol symbol)
    {
        return Math.Abs(symbol.Y - partNumber.Y) <= 1 && partNumber.X.Any(partNumberX => Math.Abs(symbol.X - partNumberX) <= 1);
    }
}

/// <summary>
/// Represents a Gondola Engine Schematic.
/// </summary>
/// <param name="Schematic">The schematic, represented as a 2D array of characters.</param>
/// <param name="PartNumbers">The possible part numbers in the schematic.</param>
/// <param name="Symbols">The symbols in the schematic.</param>
public sealed record GondolaEngineSchematic(char[,] Schematic, IReadOnlyCollection<GondolaEngineSchematicPart> PartNumbers, IReadOnlyCollection<GondolaEngineSchematicSymbol> Symbols);

/// <summary>
/// Represents an engine schematic part number.
/// </summary>
/// <param name="Number">The part number from the schematic.</param>
/// <param name="Y">The Y coordinate of the number in the schematic.</param>
/// <param name="X">The X coordinates of the number in the schematic.</param>
public sealed record GondolaEngineSchematicPart(int Number, int Y, int[] X);

/// <summary>
/// Represents a Gondola Engine Schematic Symbol.
/// </summary>
/// <param name="Symbol">The symbol.</param>
/// <param name="Y">The Y coordinate of the symbol in the schematic.</param>
/// <param name="X">The X coordinate of the symbol in the schematic.</param>
public sealed record GondolaEngineSchematicSymbol(char Symbol, int Y, int X);