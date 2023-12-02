// <copyright file="ColoredCubeGameService.cs" company="Zearain">
// Copyright (c) Zearain. All rights reserved.
// </copyright>

using System.Globalization;

using Zearain.AoC23.Domain.AdventDayAggregate.ValueObjects;

namespace Zearain.AoC23.Application.AdventDays.Services;

/// <summary>
/// Represents the service for the Colored Cube Game (Day 2).
/// </summary>
public sealed class ColoredCubeGameService
{
    private const string Red = "red";
    private const string Green = "green";
    private const string Blue = "blue";

    /// <summary>
    /// Sums the possible game IDs for the diven input.
    /// </summary>
    /// <param name="input">The given input.</param>
    /// <param name="maxRed">The maximum number of red cubes in a set.</param>
    /// <param name="maxGreen">The maximum number of green cubes in a set.</param>
    /// <param name="maxBlue">The maximum number of blue cubes in a set.</param>
    /// <returns>The sum of the possible game IDs.</returns>
    public static int SumPossibleGameIds(DayInput input, int maxRed, int maxGreen, int maxBlue)
    {
        var games = input.Lines.Select(ParseGame).ToArray();
        return games.Where(game => IsGamePossible(game, maxRed, maxGreen, maxBlue)).Sum(game => game.GameId);
    }

    /// <summary>
    /// Determines whether the given game is possible.
    /// </summary>
    /// <param name="game">The game to check.</param>
    /// <param name="maxRed">The maximum number of red cubes in a set.</param>
    /// <param name="maxGreen">The maximum number of green cubes in a set.</param>
    /// <param name="maxBlue">The maximum number of blue cubes in a set.</param>
    /// <returns>True if the game is possible, false otherwise.</returns>
    public static bool IsGamePossible(ColoredCubeGame game, int maxRed, int maxGreen, int maxBlue)
    {
        return game.Sets.All(set => IsSetPossible(set, maxRed, maxGreen, maxBlue));
    }

    /// <summary>
    /// Parses the game from the given input line.
    /// </summary>
    /// <param name="input">The input line.</param>
    /// <returns>The parsed game.</returns>
    public static ColoredCubeGame ParseGame(string input)
    {
        var game = input.Split(":");
        var gameId = int.Parse(game[0].Split(' ')[1], CultureInfo.InvariantCulture);
        var sets = game[1].Split(";").Select(ParseSet).ToArray();
        return new ColoredCubeGame(gameId, sets);
    }

    private static ColoredCubeSet ParseSet(string input)
    {
        var draws = input.Split(",").ToArray();
        var red = 0;
        var green = 0;
        var blue = 0;
        for (var i = 0; i < draws.Length; i++)
        {
            var draw = draws[i].Trim();
            var color = draw.Split(' ')[1];
            var amount = int.Parse(draw.Split(' ')[0], CultureInfo.InvariantCulture);
            switch (color)
            {
                case Red:
                    red += amount;
                    break;
                case Green:
                    green += amount;
                    break;
                case Blue:
                    blue += amount;
                    break;
            }
        }

        return new ColoredCubeSet(red, green, blue);
    }

    private static bool IsSetPossible(ColoredCubeSet set, int maxRed, int maxGreen, int maxBlue)
    {
        return set.Red <= maxRed && set.Green <= maxGreen && set.Blue <= maxBlue;
    }
}