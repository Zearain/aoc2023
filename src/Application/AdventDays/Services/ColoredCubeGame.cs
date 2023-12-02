// <copyright file="ColoredCubeGame.cs" company="Zearain">
// Copyright (c) Zearain. All rights reserved.
// </copyright>

namespace Zearain.AoC23.Application.AdventDays.Services;

/// <summary>
/// Represents a Colored Cube Game.
/// </summary>
/// <param name="GameId">The ID of the game played.</param>
/// <param name="Sets">The sets of colored cubes played.</param>
public sealed record ColoredCubeGame(int GameId, ColoredCubeSet[] Sets);