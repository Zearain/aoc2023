// <copyright file="PartSolutionAdded.cs" company="Zearain">
// Copyright (c) Zearain. All rights reserved.
// </copyright>

using Zearain.AoC23.Domain.AdventDayAggregate.ValueObjects;
using Zearain.AoC23.Domain.Common;

namespace Zearain.AoC23.Domain;

/// <summary>
/// Represents the event raised when a <see cref="PartSolution"/> is added to an <see cref="AdventDayAggregate.AdventDay"/>.
/// </summary>
/// <param name="AdventDayId">The identity of the <see cref="AdventDayAggregate.AdventDay"/>.</param>
/// <param name="DayNumber">The day number of the <see cref="AdventDayAggregate.AdventDay"/>.</param>
/// <param name="PartSolution">The Added <see cref="PartSolution"/>.</param>
public record class PartSolutionAdded(AdventDayId AdventDayId, int DayNumber, PartSolution PartSolution) : IDomainEvent;