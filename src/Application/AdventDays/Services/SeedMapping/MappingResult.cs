// <copyright file="MappingResult.cs" company="Zearain">
// Copyright (c) Zearain. All rights reserved.
// </copyright>

namespace Zearain.AoC23.Application.AdventDays.Services.SeedMapping;

/// <summary>
/// Represents a seed almanac mapping result.
/// </summary>
/// <param name="Type">The type of the mapping.</param>
/// <param name="Value">The value of the mapping.</param>
public record MappingResult(string Type, int Value);