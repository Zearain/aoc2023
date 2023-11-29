// <copyright file="AggregateRootId.cs" company="Zearain">
// Copyright (c) Zearain. All rights reserved.
// </copyright>

namespace Zearain.AoC23.Domain.Common;

/// <summary>
/// Represents the base class for aggregate root identifiers.
/// </summary>
/// <typeparam name="TId">The type of the identifier.</typeparam>
public abstract class AggregateRootId<TId> : ValueObject
    where TId : notnull
{
    /// <summary>
    /// Gets or sets the value of the identifier.
    /// </summary>
    public abstract TId Value { get; protected set; }

    /// <inheritdoc/>
    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return this.Value;
    }
}