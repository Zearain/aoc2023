// <copyright file="BoatRaceService.cs" company="Zearain">
// Copyright (c) Zearain. All rights reserved.
// </copyright>

using System.Globalization;

using Zearain.AoC23.Domain.AdventDayAggregate.ValueObjects;

namespace Zearain.AoC23.Application.AdventDays.Services.BoatRaces;

/// <summary>
/// Represents a service used to read race durations and calculate winning strategies.
/// </summary>
public class BoatRaceService
{
    /// <summary>
    /// Calculates the distance traveled from the acceleration and race time.
    /// </summary>
    /// <param name="acceleration">The acceleration in millimeters per millisecond squared.</param>
    /// <param name="raceTime">The total race time.</param>
    /// <returns>The distance traveled in millimeters.</returns>
    public static long GetDistanceFromAcceleration(long acceleration, long raceTime)
    {
        if (acceleration >= raceTime)
        {
            return 0;
        }

        var time = raceTime - acceleration;

        return acceleration * time * time / 2;
    }

    /// <summary>
    /// Calculates the distance traveled from the acceleration and race time.
    /// </summary>
    /// <param name="initialVelocity">The initial velocity in millimeters per millisecond.</param>
    /// <param name="raceTime">The total race time.</param>
    /// <returns>The distance traveled in millimeters.</returns>
    public static long GetDistanceFromInitialVelocity(long initialVelocity, long raceTime)
    {
        if (initialVelocity >= raceTime)
        {
            return 0;
        }

        var time = raceTime - initialVelocity;

        return initialVelocity * time;
    }

    /// <summary>
    /// Calculates the minimum initial velocity required to travel the given distance in the given time.
    /// </summary>
    /// <param name="distance">The distance to travel in millimeters.</param>
    /// <param name="totalTime">The total time to travel in milliseconds.</param>
    /// <returns>The minimum initial velocity in millimeters per millisecond.</returns>
    public static long GetMinimumInitialVelocity(long distance, long totalTime)
    {
        double initialVelocity = 0.0;
        double epsilon = 0.0001;
        double function, derivative;

        do
        {
            function = (initialVelocity * (totalTime - initialVelocity)) - distance;
            derivative = totalTime - (2 * initialVelocity);

            // Newton-Raphson method
            initialVelocity = initialVelocity - (function / derivative);
        }
        while (Math.Abs(function) > epsilon);

        var probablyInitialVelocity = (long)Math.Ceiling(initialVelocity);
        if (GetDistanceFromInitialVelocity(probablyInitialVelocity, totalTime) == distance)
        {
            return probablyInitialVelocity + 1;
        }

        return probablyInitialVelocity;
    }

    /// <summary>
    /// Calculates the minimum acceleration required to travel the given distance in the given time.
    /// </summary>
    /// <param name="distance">The distance to travel in millimeters.</param>
    /// <param name="totalTime">The total time to travel in milliseconds.</param>
    /// <returns>The minimum acceleration in millimeters per millisecond.</returns>
    public static long GetMinimumAcceleration(long distance, long totalTime)
    {
        return (long)Math.Ceiling((Math.Sqrt((4 * distance) + (totalTime * totalTime)) - totalTime) / 2);
    }

    /// <summary>
    /// Gets all ways to beat the record distance of a boat race given that we are looking for initial velocity.
    /// </summary>
    /// <param name="boatRace">The boat race.</param>
    /// <returns>An array of initial velocities that will beat the record distance.</returns>
    public static long[] GetWaysToWinRaceWithVelocity(BoatRace boatRace)
    {
        var minimumInitialVelocity = GetMinimumInitialVelocity(boatRace.RecordDistance, boatRace.RaceTime);
        var maximumInitialVelocity = boatRace.RaceTime - minimumInitialVelocity;

        var waysToWin = new List<long>();
        for (var i = minimumInitialVelocity; i <= maximumInitialVelocity; i++)
        {
            waysToWin.Add(i);
        }

#pragma warning disable SA1010 // Opening square brackets should be spaced correctly
        return [.. waysToWin];
#pragma warning restore SA1010 // Opening square brackets should be spaced correctly
    }

    /// <summary>
    /// Reads the boat races from the input.
    /// </summary>
    /// <param name="input">The input.</param>
    /// <returns>An array of boat races.</returns>
    public static BoatRace[] ReadBoatRaces(DayInput input)
    {
        var boatRaces = new List<BoatRace>();

        var lines = input.Lines;
        var raceTimes = lines[0].Split(':')[1].Trim().Split(' ').Where(x => !string.IsNullOrWhiteSpace(x)).Select(long.Parse).ToArray();
        var recordDistances = lines[1].Split(':')[1].Trim().Split(' ').Where(x => !string.IsNullOrWhiteSpace(x)).Select(long.Parse).ToArray();

        for (var i = 0; i < raceTimes.Length; i++)
        {
            boatRaces.Add(new BoatRace(raceTimes[i], recordDistances[i]));
        }

#pragma warning disable SA1010 // Opening square brackets should be spaced correctly
        return [.. boatRaces];
#pragma warning restore SA1010 // Opening square brackets should be spaced correctly
    }

    /// <summary>
    /// Gets a single boat race from the badly kerned input.
    /// </summary>
    /// <param name="input">The input.</param>
    /// <returns>Boat race.</returns>
    public static BoatRace ReadBoatRace(DayInput input)
    {
        var lines = input.Lines;
        var raceTime = long.Parse(lines[0].Split(':')[1].Trim().Replace(" ", string.Empty), CultureInfo.InvariantCulture);
        var recordDistance = long.Parse(lines[1].Split(':')[1].Trim().Replace(" ", string.Empty), CultureInfo.InvariantCulture);

        return new BoatRace(raceTime, recordDistance);
    }
}