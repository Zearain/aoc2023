namespace Zearain.AoC23.Domain.Common;

/// <summary>
/// Represents an entity that contains domain events.
/// </summary>
public interface IDomainEventContainer
{
    /// <summary>
    /// Gets the collection of domain events.
    /// </summary>
    IReadOnlyCollection<IDomainEvent> DomainEvents { get; }

    /// <summary>
    /// Adds a domain event to the collection.
    /// </summary>
    /// <param name="domainEvent">The domain event to add.</param>
    void AddDomainEvent(IDomainEvent domainEvent);

    /// <summary>
    /// Clears the collection of domain events.
    /// </summary>
    void ClearDomainEvents();
}
