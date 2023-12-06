// <copyright file="BoatRace.cs" company="Zearain">
// Copyright (c) Zearain. All rights reserved.
// </copyright>

namespace Zearain.AoC23.Application.AdventDays.Services.BoatRaces;

/// <summary>
/// Represents a boat race.
/// </summary>
/// <param name="RaceTime">The total time of the race in milliseconds.</param>
/// <param name="RecordDistance">The record distance traveled in millimeters.</param>
public sealed record BoatRace(long RaceTime, long RecordDistance);