// <copyright file="ColoredCubeGameServiceTests.cs" company="Zearain">
// Copyright (c) Zearain. All rights reserved.
// </copyright>

namespace Zearain.AoC23.Application.Tests;

/// <summary>
/// Tests for <see cref="ColoredCubeGameService"/>.
/// </summary>
[TestFixture(Description = "Tests for ColoredCubeGameService")]
public class ColoredCubeGameServiceTests
{
    private const string RawTestInput = @"Game 1: 3 blue, 4 red; 1 red, 2 green, 6 blue; 2 green
Game 2: 1 blue, 2 green; 3 green, 4 blue, 1 red; 1 green, 1 blue
Game 3: 8 green, 6 blue, 20 red; 5 blue, 4 red, 13 green; 5 green, 1 red
Game 4: 1 green, 3 red, 6 blue; 3 green, 6 red; 3 green, 15 blue, 14 red
Game 5: 6 red, 1 blue, 3 green; 2 blue, 1 red, 2 green";

    private static readonly DayInput TestInput = DayInput.Create(RawTestInput);
    private static readonly ColoredCubeGame[] TestGames = new[]
    {
        new ColoredCubeGame(
            1, new[]
            {
                new ColoredCubeSet(4, 0, 3),
                new ColoredCubeSet(1, 2, 6),
                new ColoredCubeSet(0, 2, 0),
            }),
        new ColoredCubeGame(
            2, new[]
            {
                new ColoredCubeSet(0, 2, 1),
                new ColoredCubeSet(1, 3, 4),
                new ColoredCubeSet(0, 1, 1),
            }),
        new ColoredCubeGame(
            3, new[]
            {
                new ColoredCubeSet(20, 8, 6),
                new ColoredCubeSet(4, 13, 5),
                new ColoredCubeSet(1, 5, 0),
            }),
        new ColoredCubeGame(
            4, new[]
            {
                new ColoredCubeSet(3, 1, 6),
                new ColoredCubeSet(6, 3, 0),
                new ColoredCubeSet(14, 3, 15),
            }),
        new ColoredCubeGame(
            5, new[]
            {
                new ColoredCubeSet(6, 3, 1),
                new ColoredCubeSet(1, 2, 2),
            }),
    };

    [TestCaseSource(nameof(ParseGameTestCases))]
    public void ParseGame_ShouldParseGameCorrectly(string input, ColoredCubeGame expected)
    {
        // Arrange
        var service = new ColoredCubeGameService();

        // Act
        var actual = ColoredCubeGameService.ParseGame(input);

        // Assert
        actual.Should().BeEquivalentTo(expected);
    }

    [TestCaseSource(nameof(IsGamePossibleTestCases))]
    public void IsGamePossible_ShouldReturnCorrectResult(ColoredCubeGame game, bool expected)
    {
        // Arrange
        var service = new ColoredCubeGameService();

        // Act
        var actual = ColoredCubeGameService.IsGamePossible(game, 12, 13, 14);

        // Assert
        actual.Should().Be(expected);
    }

    [Test(Description = "Should return the sum of the game IDs of all possible games")]
    public void SumPossibleGameIds_ShouldReturnCorrectResult()
    {
        // Arrange
        var service = new ColoredCubeGameService();

        // Act
        var actual = ColoredCubeGameService.SumPossibleGameIds(TestInput, 12, 13, 14);

        // Assert
        actual.Should().Be(8);
    }

    [TestCaseSource(nameof(MinimumSetTestCases))]
    public void ColoredCubeGameMinimumSet_ShouldReturnCorrectResult(ColoredCubeGame game, ColoredCubeSet expected, int expectedSetPower)
    {
        // Arrange
        var service = new ColoredCubeGameService();

        // Act
        var actual = game.MinimumSet;

        // Assert
        actual.Should().BeEquivalentTo(expected);
        actual.Power.Should().Be(expectedSetPower);
    }

    [Test(Description = "Should return the sum of the power of the minimal sets of all games")]
    public void SumMinimalSetsPower_ShouldReturnCorrectResult()
    {
        // Arrange
        var service = new ColoredCubeGameService();

        // Act
        var actual = ColoredCubeGameService.SumMinimalSetsPower(TestInput);

        // Assert
        actual.Should().Be(2286);
    }

    private static IEnumerable<TestCaseData> ParseGameTestCases()
    {
        for (var i = 0; i < TestInput.Lines.Length; i++)
        {
            yield return new TestCaseData(TestInput.Lines[i], TestGames[i]);
        }
    }

    private static IEnumerable<TestCaseData> IsGamePossibleTestCases()
    {
        yield return new TestCaseData(TestGames[0], true);
        yield return new TestCaseData(TestGames[1], true);
        yield return new TestCaseData(TestGames[2], false);
        yield return new TestCaseData(TestGames[3], false);
        yield return new TestCaseData(TestGames[4], true);
    }

    private static IEnumerable<TestCaseData> MinimumSetTestCases()
    {
        yield return new TestCaseData(TestGames[0], new ColoredCubeSet(4, 2, 6), 48);
        yield return new TestCaseData(TestGames[1], new ColoredCubeSet(1, 3, 4), 12);
        yield return new TestCaseData(TestGames[2], new ColoredCubeSet(20, 13, 6), 1560);
        yield return new TestCaseData(TestGames[3], new ColoredCubeSet(14, 3, 15), 630);
        yield return new TestCaseData(TestGames[4], new ColoredCubeSet(6, 3, 2), 36);
    }
}