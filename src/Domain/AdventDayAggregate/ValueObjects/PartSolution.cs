﻿// <copyright file="PartSolution.cs" company="Zearain">
// Copyright (c) Zearain. All rights reserved.
// </copyright>

using ErrorOr;

using Zearain.AoC23.Domain.AdventDayAggregate.Errors;
using Zearain.AoC23.Domain.Common;

namespace Zearain.AoC23.Domain.AdventDayAggregate.ValueObjects;

/// <summary>
/// Represents the solution to a part of an Advent Day.
/// </summary>
public sealed class PartSolution : ValueObject
{
    private PartSolution()
    {
    }

    private PartSolution(int partNumber, string solution)
    {
        this.PartNumber = partNumber;
        this.Solution = solution;
    }

    /// <summary>
    /// Gets the part number.
    /// </summary>
    public int PartNumber { get; private set; }

    /// <summary>
    /// Gets the solution to the part.
    /// </summary>
    public string Solution { get; private set; } = string.Empty;

    /// <summary>
    /// Creates a new <see cref="PartSolution"/>.
    /// </summary>
    /// <param name="partNumber">The part number.</param>
    /// <param name="solution">The solution to the part.</param>
    /// <returns>A new instance of <see cref="PartSolution"/>.</returns>
    public static ErrorOr<PartSolution> Create(int partNumber, string solution)
    {
        if (partNumber < 1 || partNumber > 2)
        {
            return AdventDayErrors.InvalidPartNumber;
        }

        return new PartSolution(partNumber, solution);
    }

    /// <inheritdoc/>
    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return this.PartNumber;
        yield return this.Solution;
    }
}