// <copyright file="AdventDay.cs" company="Zearain">
// Copyright (c) Zearain. All rights reserved.
// </copyright>

using Zearain.AoC23.Domain.AdventDayAggregate.Entities;
using Zearain.AoC23.Domain.AdventDayAggregate.Events;
using Zearain.AoC23.Domain.AdventDayAggregate.ValueObjects;
using Zearain.AoC23.Domain.Common;

namespace Zearain.AoC23.Domain.AdventDayAggregate;

/// <summary>
/// Represents an advent day.
/// </summary>
public class AdventDay : AggregateRoot<AdventDayId, Guid>
{
    /// <summary>
    /// Gets a value indicating whether the <see cref="AdventDay"/> has an input.
    /// </summary>
    public bool HasInput { get; private set; }

    /// <summary>
    /// Gets the input of the <see cref="AdventDay"/>.
    /// </summary>
    public DayInput? Input { get; private set; }

    /// <summary>
    /// Sets the input of the <see cref="AdventDay"/>.
    /// </summary>
    /// <param name="input">The input to set.</param>
    public void SetInput(DayInput input)
    {
        this.Input = input;

        this.HasInput = this.Input is not null;

        this.AddDomainEvent(new DayInputAdded(AdventDayId.Create(this.Id.Value), input));
    }
}