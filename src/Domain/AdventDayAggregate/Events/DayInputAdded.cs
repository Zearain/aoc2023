﻿// <copyright file="DayInputAdded.cs" company="Zearain">
// Copyright (c) Zearain. All rights reserved.
// </copyright>

using Zearain.AoC23.Domain.AdventDayAggregate.ValueObjects;
using Zearain.AoC23.Domain.Common;

namespace Zearain.AoC23.Domain.AdventDayAggregate.Events;

/// <summary>
/// Represents the event raised when a <see cref="DayInput"/> is added to an <see cref="AdventDay"/>.
/// </summary>
/// <param name="AdventDayId">The identity of the <see cref="AdventDay"/>.</param>
/// <param name="AddedInput">The Added <see cref="DayInput"/>.</param>
/// <param name="HasInput">A value indicating whether the <see cref="AdventDay"/> input is valid.</param>
public record DayInputAdded(AdventDayId AdventDayId, DayInput AddedInput, bool HasInput) : IDomainEvent;