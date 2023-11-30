// <copyright file="DateTimeProvider.cs" company="Zearain">
// Copyright (c) Zearain. All rights reserved.
// </copyright>

using Zearain.AoC23.Application.Abstractions;

namespace Zearain.AoC23.Infrastructure.Services;

/// <inheritdoc/>
public class DateTimeProvider : IDateTimeProvider
{
    /// <inheritdoc/>
    public DateTimeOffset UtcNow => DateTimeOffset.UtcNow;
}