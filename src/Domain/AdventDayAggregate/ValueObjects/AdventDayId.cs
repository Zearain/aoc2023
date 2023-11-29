// <copyright file="AdventDayId.cs" company="Zearain">
// Copyright (c) Zearain. All rights reserved.
// </copyright>

using Zearain.AoC23.Domain.Common;

namespace Zearain.AoC23.Domain.AdventDayAggregate.ValueObjects;

/// <summary>
/// Represents the identity of an <see cref="AdventDay"/>.
/// </summary>
public sealed class AdventDayId : AggregateRootId<Guid>
{
    private AdventDayId()
    {
    }

    private AdventDayId(Guid value)
    {
        this.Value = value;
    }

    /// <inheritdoc/>
    public override Guid Value { get; protected set; }

    /// <summary>
    /// Generates a new <see cref="AdventDayId"/> instance.
    /// </summary>
    /// <returns>A new <see cref="AdventDayId"/> instance.</returns>
    public static AdventDayId New() => new AdventDayId(Guid.NewGuid());

    /// <summary>
    /// Creates a new <see cref="AdventDayId"/> instance with the specified value.
    /// </summary>
    /// <param name="value">The value of the <see cref="AdventDayId"/>.</param>
    /// <returns>A new <see cref="AdventDayId"/> instance with the specified value.</returns>
    public static AdventDayId Create(Guid value) => new AdventDayId(value);
}