// <copyright file="AdventDayCreated.cs" company="Zearain">
// Copyright (c) Zearain. All rights reserved.
// </copyright>

using Zearain.AoC23.Domain.Common;

namespace Zearain.AoC23.Domain.AdventDayAggregate.Events;

/// <summary>
/// Represents the event raised when a <see cref="AdventDay"/> is created.
/// </summary>
/// <param name="AdventDay">The created <see cref="AdventDay"/>.</param>
public record class AdventDayCreated(AdventDay AdventDay) : IDomainEvent;