// <copyright file="ColoredCubeSet.cs" company="Zearain">
// Copyright (c) Zearain. All rights reserved.
// </copyright>

namespace Zearain.AoC23.Application.AdventDays.Services;

/// <summary>
/// Represents a set of colored cubes.
/// </summary>
/// <param name="Red">Number of red cubes.</param>
/// <param name="Green">Number of green cubes.</param>
/// <param name="Blue">Number of blue cubes.</param>
public sealed record ColoredCubeSet(int Red, int Green, int Blue)
{
    /// <summary>
    /// Gets the power of the set.
    /// </summary>
    public int Power { get; } = Red * Green * Blue;
}