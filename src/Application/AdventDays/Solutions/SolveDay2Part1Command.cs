// <copyright file="SolveDay2Part1Command.cs" company="Zearain">
// Copyright (c) Zearain. All rights reserved.
// </copyright>

using Zearain.AoC23.Application.Abstractions;
using Zearain.AoC23.Application.Attributes;
using Zearain.AoC23.Domain.AdventDayAggregate.ValueObjects;

namespace Zearain.AoC23.Application.AdventDays.Solutions;

/// <inheritdoc />
[AdventDayPart(2, 1)]
public record SolveDay2Part1Command(int PartNumber, DayInput Input) : AdventDayPartSolutionRequest(PartNumber, Input);