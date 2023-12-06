// <copyright file="BoatRaceServiceTests.cs" company="Zearain">
// Copyright (c) Zearain. All rights reserved.
// </copyright>

using Zearain.AoC23.Application.AdventDays.Services.BoatRaces;

namespace Zearain.AoC23.Application.Tests.Services;

[TestFixture]
public class BoatRaceServiceTests
{
    private const string RawTestInput = @"Time:      7  15   30
Distance:  9  40  200";

    private static readonly DayInput TestInput = DayInput.Create(RawTestInput);

    [TestCaseSource(nameof(BoatRaceDistanceTestCases))]
    public void BoatRaceService_GetDistanceFromInitialVelocity_ReturnsCorrectDistance(int initialVelocity, int raceTime, int expectedDistance)
    {
        // Act
        var distance = BoatRaceService.GetDistanceFromInitialVelocity(initialVelocity, raceTime);

        // Assert
        distance.Should().Be(expectedDistance);
    }

    [TestCaseSource(nameof(BoatRaceInitialVelocityTestCases))]
    public void BoatRaceService_GetMinimumInitialVelocity_ReturnsCorrectAcceleration(int distance, int totalTime, int expectedInitalVelocity)
    {
        // Act
        var initalVelocity = BoatRaceService.GetMinimumInitialVelocity(distance, totalTime);

        // Assert
        initalVelocity.Should().Be(expectedInitalVelocity);
    }

    [TestCaseSource(nameof(BoatRaceWaysToWinVelocityTestCases))]
    public void BoatRaceService_GetWaysToWin_ReturnsCorrectNumberOfWaysToWin(BoatRace boatRace, int expectedWaysToWin)
    {
        // Act
        var waysToWin = BoatRaceService.GetWaysToWinRaceWithVelocity(boatRace);

        // Assert
        waysToWin.Length.Should().Be(expectedWaysToWin);
    }

    private static IEnumerable<TestCaseData> BoatRaceDistanceTestCases()
    {
        yield return new TestCaseData(0, 7, 0);
        yield return new TestCaseData(1, 7, 6);
        yield return new TestCaseData(2, 7, 10);
        yield return new TestCaseData(3, 7, 12);
        yield return new TestCaseData(4, 7, 12);
        yield return new TestCaseData(5, 7, 10);
        yield return new TestCaseData(6, 7, 6);
        yield return new TestCaseData(7, 7, 0);
    }

    private static IEnumerable<TestCaseData> BoatRaceInitialVelocityTestCases()
    {
        yield return new TestCaseData(9, 7, 2);
    }

    private static IEnumerable<TestCaseData> BoatRaceWaysToWinVelocityTestCases()
    {
        yield return new TestCaseData(new BoatRace(7, 9), 4);
        yield return new TestCaseData(new BoatRace(15, 40), 8);
        yield return new TestCaseData(new BoatRace(30, 200), 9);
    }
}