// <copyright file="IAdventDayPartSolutionRequest.cs" company="Zearain">
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
public interface IAdventDayPartSolutionRequest : IRequest<ErrorOr<PartSolution>>
{
    /// <summary>
    /// Gets the part number.
    /// </summary>
    int PartNumber { get; }

    /// <summary>
    /// Gets the input to use to calculate the solution.
    /// </summary>
    DayInput Input { get; }
}