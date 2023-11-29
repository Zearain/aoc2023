// <copyright file="IDomainEvent.cs" company="Zearain">
// Copyright (c) Zearain. All rights reserved.
// </copyright>

using MediatR;

namespace Zearain.AoC23.Domain.Common;

/// <summary>
/// Represents a domain event.
/// </summary>
public interface IDomainEvent : INotification
{
}