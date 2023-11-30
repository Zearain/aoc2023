// <copyright file="GetAdventDayQuery.cs" company="Zearain">
// Copyright (c) Zearain. All rights reserved.
// </copyright>

using ErrorOr;

using MediatR;

using Zearain.AoC23.Domain.AdventDayAggregate;
using Zearain.AoC23.Domain.AdventDayAggregate.ValueObjects;

namespace Zearain.AoC23.Application.AdventDays.Queries;

/// <summary>
/// Represents a query to get an Advent Day.
/// </summary>
/// <param name="Id">The ID of the Advent Day.</param>
public record GetAdventDayQuery(AdventDayId Id) : IRequest<ErrorOr<AdventDay>>;