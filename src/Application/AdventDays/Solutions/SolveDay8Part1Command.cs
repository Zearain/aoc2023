// <copyright file="SolveDay8Part1Command.cs" company="Zearain">
// Copyright (c) Zearain. All rights reserved.
// </copyright>

using Zearain.AoC23.Application.Abstractions;
using Zearain.AoC23.Application.Attributes;
using Zearain.AoC23.Domain.AdventDayAggregate.ValueObjects;

namespace Zearain.AoC23.Application.AdventDays.Solutions;

/// <summary>
/// Represents the command to solve the first part of day 8.
/// </summary>
/// <param name="PartNumber">The part number to solve.</param>
/// <param name="Input">The input to solve.</param>
[AdventDayPart(8, 1)]
public record SolveDay8Part1Command(int PartNumber, DayInput Input) : AdventDayPartSolutionRequest(PartNumber, Input);