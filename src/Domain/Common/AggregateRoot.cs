// <copyright file="AggregateRoot.cs" company="Zearain">
// Copyright (c) Zearain. All rights reserved.
// </copyright>

namespace Zearain.AoC23.Domain.Common;

/// <summary>
/// Represents an aggregate root entity in the domain model.
/// </summary>
/// <typeparam name="TId">The type of the aggregate root's identifier.</typeparam>
/// <typeparam name="TIdType">The type of the aggregate root's identifier's value.</typeparam>
public abstract class AggregateRoot<TId, TIdType> : Entity<TId>
    where TId : AggregateRootId<TIdType>
    where TIdType : notnull
{
    /// <summary>
    /// Initializes a new instance of the <see cref="AggregateRoot{TId, TIdType}"/> class with the specified identifier.
    /// </summary>
    /// <param name="id">The identifier of the aggregate root.</param>
    protected AggregateRoot(TId id)
    {
        this.Id = id;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="AggregateRoot{TId, TIdType}"/> class.
    /// </summary>
#pragma warning disable CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.
    protected AggregateRoot()
    {
    }
#pragma warning restore CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.

    /// <summary>
    /// Gets or sets the identifier of the aggregate root.
    /// </summary>

#pragma warning disable CA1061 // Do not hide base class methods
    public new AggregateRootId<TIdType> Id { get; protected set; }
#pragma warning restore CA1061 // Do not hide base class methods
}