// <copyright file="GondolaEngineSchematic.cs" company="Zearain">
// Copyright (c) Zearain. All rights reserved.
// </copyright>
namespace Zearain.AoC23.Application.AdventDays.Services.GondolaEngineSchematics;

/// <summary>
/// Represents a Gondola Engine Schematic.
/// </summary>
/// <param name="Schematic">The schematic, represented as a 2D array of characters.</param>
/// <param name="PartNumbers">The possible part numbers in the schematic.</param>
/// <param name="Symbols">The symbols in the schematic.</param>
public sealed record GondolaEngineSchematic(char[,] Schematic, IReadOnlyCollection<GondolaEngineSchematicPart> PartNumbers, IReadOnlyCollection<GondolaEngineSchematicSymbol> Symbols)
{
    /// <summary>
    /// Gets the gear ratios from the schematic.
    /// </summary>
    /// <returns>The gear ratios.</returns>
    public int[] GetGearRatios()
    {
        var gears = this.Symbols.Where(s => s.Symbol == '*' && this.PartNumbers.Where(p => p.IsAdjacent(s)).Count() == 2).ToArray();
        var gearRatios = new int[gears.Length];
        for (var i = 0; i < gears.Length; i++)
        {
            var gear = gears[i];
            var adjacentPartNumbers = this.PartNumbers.Where(p => p.IsAdjacent(gear)).ToArray();
            gearRatios[i] = adjacentPartNumbers[0].Number * adjacentPartNumbers[1].Number;
        }

        return gearRatios;
    }
}