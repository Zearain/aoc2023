// <copyright file="AdventDayPartAttribute.cs" company="Zearain">
// Copyright (c) Zearain. All rights reserved.
// </copyright>

namespace Zearain.AoC23.Application.Attributes;

/// <summary>
/// Represents an attribute to mark a class as an Advent Day part.
/// </summary>
[AttributeUsage(AttributeTargets.Class)]
public sealed class AdventDayPartAttribute : Attribute
{
    /// <summary>
    /// Gets the day number.
    /// </summary>
    public int DayNumber { get; }

    /// <summary>
    /// Gets the part number.
    /// </summary>
    public int PartNumber { get; }
}