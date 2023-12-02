// <copyright file="AdventDayPartSolutionRequest.cs" company="Zearain">
// Copyright (c) Zearain. All rights reserved.
// </copyright>

using ErrorOr;

using MediatR;

using Zearain.AoC23.Domain.AdventDayAggregate;
using Zearain.AoC23.Domain.AdventDayAggregate.Entities;
using Zearain.AoC23.Domain.AdventDayAggregate.ValueObjects;

namespace Zearain.AoC23.Application.Abstractions;

/// <summary>
/// Represents a request to calculate the <see cref="PartSolution"/> to a part of an <see cref="AdventDay"/>.
/// </summary>
/// <param name="PartNumber">The part number to solve.</param>
/// <param name="Input">The input to solve.</param>
public abstract record AdventDayPartSolutionRequest(int PartNumber, DayInput Input) : IRequest<ErrorOr<PartSolution>>
{
}