// <copyright file="AdventDayFileInput.cs" company="Zearain">
// Copyright (c) Zearain. All rights reserved.
// </copyright>

namespace Zearain.AoC23.Contracts.Requests;

/// <summary>
/// Represents the input for an Advent Day.
/// </summary>
public record AdventDayFileInput
{
    /// <summary>
    /// Gets the ID of the Advent Day this input is for.
    /// </summary>
    public Guid AdventDayId { get; init; }

    /// <summary>
    /// Gets the file as a byte array.
    /// </summary>
    public byte[] File { get; init; } = null!;
}