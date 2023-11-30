// <copyright file="IDateTimeProvider.cs" company="Zearain">
// Copyright (c) Zearain. All rights reserved.
// </copyright>

namespace Zearain.AoC23.Application.Abstractions;

/// <summary>
/// Represents a provider for the current date and time.
/// </summary>
public interface IDateTimeProvider
{
    /// <summary>
    /// Gets the current date and time in UTC.
    /// </summary>
    DateTimeOffset UtcNow { get; }
}