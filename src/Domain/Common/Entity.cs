// <copyright file="Entity.cs" company="Zearain">
// Copyright (c) Zearain. All rights reserved.
// </copyright>

namespace Zearain.AoC23.Domain.Common;

/// <summary>
/// Represents an entity with a unique identifier of type <typeparamref name="TId"/>.
/// </summary>
/// <typeparam name="TId">The type of the unique identifier.</typeparam>
public abstract class Entity<TId> : IEquatable<Entity<TId>>, IDomainEventContainer
    where TId : notnull
{
    private readonly List<IDomainEvent> domainEvents = new();

    /// <summary>
    /// Initializes a new instance of the <see cref="Entity{TId}"/> class with the specified identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the entity.</param>
    protected Entity(TId id)
    {
        this.Id = id;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Entity{TId}"/> class.
    /// </summary>
#pragma warning disable CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.
    protected Entity()
    {
    }
#pragma warning restore CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.

    /// <summary>
    /// Gets or sets the unique identifier of the entity.
    /// </summary>
    public TId Id { get; protected set; }

    /// <inheritdoc/>
    public IReadOnlyCollection<IDomainEvent> DomainEvents => this.domainEvents.AsReadOnly();

    /// <summary>
    /// Determines whether two specified entities are equal.
    /// </summary>
    /// <param name="left">The first entity to compare.</param>
    /// <param name="right">The second entity to compare.</param>
    /// <returns>true if the two entities are equal; otherwise, false.</returns>
    public static bool operator ==(Entity<TId> left, Entity<TId> right)
    {
        return Equals(left, right);
    }

    /// <summary>
    /// Determines whether two specified entities are not equal.
    /// </summary>
    /// <param name="left">The first entity to compare.</param>
    /// <param name="right">The second entity to compare.</param>
    /// <returns>true if the two entities are not equal; otherwise, false.</returns>
    public static bool operator !=(Entity<TId> left, Entity<TId> right)
    {
        return !Equals(left, right);
    }

    /// <inheritdoc/>
    public override bool Equals(object? obj)
    {
        return obj is Entity<TId> entity && entity.Id.Equals(this.Id);
    }

    /// <inheritdoc/>
    public override int GetHashCode()
    {
        return this.Id.GetHashCode();
    }

    /// <inheritdoc/>
    public bool Equals(Entity<TId>? other)
    {
        return this.Equals((object?)other);
    }

    /// <inheritdoc/>
    public void AddDomainEvent(IDomainEvent domainEvent)
    {
        this.domainEvents.Add(domainEvent);
    }

    /// <inheritdoc/>
    public void ClearDomainEvents()
    {
        this.domainEvents.Clear();
    }
}