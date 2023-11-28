namespace Zearain.AoC23.Domain.Common;

/// <summary>
/// Represents the base class for aggregate root identifiers.
/// </summary>
/// <typeparam name="TId">The type of the identifier.</typeparam>
public abstract record AggregateRootId<TId> : ValueObject
    where TId : notnull
{
    /// <summary>
    /// Gets or sets the value of the identifier.
    /// </summary>
    public abstract TId Value { get; protected set; }
}
