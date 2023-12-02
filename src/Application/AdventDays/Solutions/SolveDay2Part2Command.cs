// <copyright file="SolveDay2Part2Command.cs" company="Zearain">
// Copyright (c) Zearain. All rights reserved.
// </copyright>

using Zearain.AoC23.Application.Abstractions;
using Zearain.AoC23.Application.Attributes;
using Zearain.AoC23.Domain.AdventDayAggregate.ValueObjects;

namespace Zearain.AoC23.Application.AdventDays.Solutions;

/// <inheritdoc />
[AdventDayPart(2, 2)]
public record SolveDay2Part2Command(int PartNumber, DayInput Input) : AdventDayPartSolutionRequest(PartNumber, Input);