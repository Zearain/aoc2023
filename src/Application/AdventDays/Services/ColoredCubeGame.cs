// <copyright file="ColoredCubeGame.cs" company="Zearain">
// Copyright (c) Zearain. All rights reserved.
// </copyright>

namespace Zearain.AoC23.Application.AdventDays.Services;

/// <summary>
/// Represents a Colored Cube Game.
/// </summary>
/// <param name="GameId">The ID of the game played.</param>
/// <param name="Sets">The sets of colored cubes played.</param>
public sealed record ColoredCubeGame(int GameId, ColoredCubeSet[] Sets)
{
    /// <summary>
    /// Gets the minimum amount of cubes needed for this game.
    /// </summary>
    public ColoredCubeSet MinimumSet { get; } = new ColoredCubeSet(Sets.Max(s => s.Red), Sets.Max(s => s.Green), Sets.Max(s => s.Blue));
}