// <copyright file="AdventDayErrors.cs" company="Zearain">
// Copyright (c) Zearain. All rights reserved.
// </copyright>

using ErrorOr;

namespace Zearain.AoC23.Domain.AdventDayAggregate.Errors;

/// <summary>
/// Contains the errors for the <see cref="AdventDayAggregate.AdventDay"/> aggregate.
/// </summary>
public static class AdventDayErrors
{
    /// <summary>
    /// Gets the error for an invalid day number.
    /// </summary>
    public static Error InvalidDayNumber => Error.Validation("AdventDay.InvalidDayNumber", "The day number must be between 1 and 25.");

    /// <summary>
    /// Gets the error for a duplicate day number.
    /// </summary>
    public static Error DuplicateDayNumber => Error.Conflict("AdventDay.DuplicateDayNumber", "An Advent Day with the given day number already exists.");

    /// <summary>
    /// Gets the error for trying to add a solution without an input.
    /// </summary>
    public static Error NoInput => Error.Validation("AdventDay.NoInput", "Cannot add a solution without an input.");

    /// <summary>
    /// Gets the error for an invalid part number.
    /// </summary>
    public static Error InvalidPartNumber => Error.Validation("AdventDay.PartSolution.InvalidPartNumber", "The part number must be 1 or 2.");

    /// <summary>
    /// Gets the error for an invalid solution order.
    /// </summary>
    public static Error InvalidSolutionOrder => Error.Validation("AdventDay.PartSolution.InvalidSolutionOrder", "The solutions for parts must be added in order.");
}