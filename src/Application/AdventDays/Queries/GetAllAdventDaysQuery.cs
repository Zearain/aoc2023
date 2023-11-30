// <copyright file="GetAllAdventDaysQuery.cs" company="Zearain">
// Copyright (c) Zearain. All rights reserved.
// </copyright>

using ErrorOr;

using MediatR;

using Zearain.AoC23.Domain.AdventDayAggregate;

namespace Zearain.AoC23.Application.AdventDays.Queries;

/// <summary>
/// Represents a query to get all <see cref="AdventDay"/>s.
/// </summary>
public record GetAllAdventDaysQuery : IRequest<ErrorOr<IEnumerable<AdventDay>>>;