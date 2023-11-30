// <copyright file="CreateAdventDayPartSolutionCommand.cs" company="Zearain">
// Copyright (c) Zearain. All rights reserved.
// </copyright>

using ErrorOr;

using MediatR;

using Zearain.AoC23.Domain.AdventDayAggregate;
using Zearain.AoC23.Domain.AdventDayAggregate.ValueObjects;

namespace Zearain.AoC23.Application;

/// <summary>
/// Represents a command to create a solution to a part of an Advent Day.
/// </summary>
/// <param name="AdventDayId">The ID of the <see cref="AdventDay"/>.</param>
/// <param name="PartNumber">The part number.</param>
public record CreateAdventDayPartSolutionCommand(AdventDayId AdventDayId, int PartNumber) : IRequest<ErrorOr<Updated>>;