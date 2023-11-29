// <copyright file="DayInput.cs" company="Zearain">
// Copyright (c) Zearain. All rights reserved.
// </copyright>

using Zearain.AoC23.Domain.Common;

namespace Zearain.AoC23.Domain.AdventDayAggregate.Entities;

/// <summary>
/// Represents the input of an advent day.
/// </summary>
public class DayInput : ValueObject
{
    /// <summary>
    /// Gets the raw text input.
    /// </summary>
    public string RawInput { get; private set; } = string.Empty;

    /// <summary>
    /// Gets the lines of the input.
    /// </summary>
    public string[] Lines => SplitLines(this.RawInput);

    /// <summary>
    /// Gets the sections of the input.
    /// </summary>
    public string[] Sections => string.IsNullOrWhiteSpace(this.RawInput) ? Array.Empty<string>() : this.RawInput.Split($"{Environment.NewLine}{Environment.NewLine}");

    /// <summary>
    /// Gets the sections of the input, split into lines.
    /// </summary>
    public string[][] SectionLines => this.Sections.Select(SplitLines).ToArray();

    /// <inheritdoc/>
    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return this.RawInput;
    }

    private static string[] SplitLines(string input) => string.IsNullOrWhiteSpace(input) ? Array.Empty<string>() : input.Split(Environment.NewLine);
}