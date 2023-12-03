// <copyright file="GondolaEngineSchematicPart.cs" company="Zearain">
// Copyright (c) Zearain. All rights reserved.
// </copyright>

namespace Zearain.AoC23.Application.AdventDays.Services.GondolaEngineSchematics;

/// <summary>
/// Represents an engine schematic part number.
/// </summary>
/// <param name="Number">The part number from the schematic.</param>
/// <param name="Y">The Y coordinate of the number in the schematic.</param>
/// <param name="X">The X coordinates of the number in the schematic.</param>
public sealed record GondolaEngineSchematicPart(int Number, int Y, int[] X);