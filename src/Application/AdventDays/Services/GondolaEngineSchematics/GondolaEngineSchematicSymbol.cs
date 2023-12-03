// <copyright file="GondolaEngineSchematicSymbol.cs" company="Zearain">
// Copyright (c) Zearain. All rights reserved.
// </copyright>

namespace Zearain.AoC23.Application.AdventDays.Services.GondolaEngineSchematics;

/// <summary>
/// Represents a Gondola Engine Schematic Symbol.
/// </summary>
/// <param name="Symbol">The symbol.</param>
/// <param name="Y">The Y coordinate of the symbol in the schematic.</param>
/// <param name="X">The X coordinate of the symbol in the schematic.</param>
public sealed record GondolaEngineSchematicSymbol(char Symbol, int Y, int X);