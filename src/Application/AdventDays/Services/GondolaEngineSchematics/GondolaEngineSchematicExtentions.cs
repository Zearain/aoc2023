// <copyright file="GondolaEngineSchematicExtentions.cs" company="Zearain">
// Copyright (c) Zearain. All rights reserved.
// </copyright>

namespace Zearain.AoC23.Application.AdventDays.Services.GondolaEngineSchematics;

/// <summary>
/// Extentions for <see cref="GondolaEngineSchematic"/>.
/// </summary>
public static class GondolaEngineSchematicExtentions
{
    /// <summary>
    /// Checks if the given part number is adjacent to the given symbol.
    /// </summary>
    /// <param name="partNumber">The part number.</param>
    /// <param name="symbol">The symbol.</param>
    /// <returns>True if the part number is adjacent to the symbol, false otherwise.</returns>
    public static bool IsAdjacent(this GondolaEngineSchematicPart partNumber, GondolaEngineSchematicSymbol symbol)
    {
        return partNumber.X.Any(partNumberX => Math.Abs(symbol.X - partNumberX) <= 1 && Math.Abs(symbol.Y - partNumber.Y) <= 1);
    }

    /// <summary>
    /// Checks if the given symbol is adjacent to the given part number.
    /// </summary>
    /// <param name="symbol">The symbol.</param>
    /// <param name="partNumber">The part number.</param>
    /// <returns>True if the symbol is adjacent to the part number, false otherwise.</returns>
    public static bool IsAdjacent(this GondolaEngineSchematicSymbol symbol, GondolaEngineSchematicPart partNumber)
    {
        return partNumber.IsAdjacent(symbol);
    }
}