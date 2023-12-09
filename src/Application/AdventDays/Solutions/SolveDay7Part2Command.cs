// <copyright file="SolveDay7Part2Command.cs" company="Zearain">
// Copyright (c) Zearain. All rights reserved.
// </copyright>

using Zearain.AoC23.Application.Abstractions;
using Zearain.AoC23.Application.Attributes;
using Zearain.AoC23.Domain.AdventDayAggregate.ValueObjects;

namespace Zearain.AoC23.Application.AdventDays.Solutions;

/// <summary>
/// Represents the command to solve the first part of day 7.
/// </summary>
/// <param name="PartNumber">The part number to solve.</param>
/// <param name="Input">The input to solve.</param>
[AdventDayPart(7, 2)]
public record SolveDay7Part2Command(int PartNumber, DayInput Input) : AdventDayPartSolutionRequest(PartNumber, Input);