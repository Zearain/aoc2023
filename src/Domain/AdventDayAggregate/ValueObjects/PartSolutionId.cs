// <copyright file="PartSolutionId.cs" company="Zearain">
// Copyright (c) Zearain. All rights reserved.
// </copyright>

using Zearain.AoC23.Domain.AdventDayAggregate.Entities;
using Zearain.AoC23.Domain.Common;

namespace Zearain.AoC23.Domain.AdventDayAggregate.ValueObjects;

/// <summary>
/// Represents the id of a <see cref="PartSolution"/>.
/// </summary>
public sealed class PartSolutionId : ValueObject
{
    private PartSolutionId()
    {
    }

    private PartSolutionId(Guid value)
    {
        this.Value = value;
    }

    /// <summary>
    /// Gets the value of the id.
    /// </summary>
    public Guid Value { get; }

    /// <summary>
    /// Generates a new <see cref="PartSolutionId"/> instance.
    /// </summary>
    /// <returns>A new <see cref="PartSolutionId"/> instance.</returns>
    public static PartSolutionId New() => new PartSolutionId(Guid.NewGuid());

    /// <summary>
    /// Creates a new <see cref="PartSolutionId"/> instance with the specified value.
    /// </summary>
    /// <param name="value">The value of the <see cref="PartSolutionId"/>.</param>
    /// <returns>A new <see cref="PartSolutionId"/> instance with the specified value.</returns>
    public static PartSolutionId Create(Guid value) => new PartSolutionId(value);

    /// <inheritdoc/>
    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return this.Value;
    }
}