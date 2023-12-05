// <copyright file="DayInput.cs" company="Zearain">
// Copyright (c) Zearain. All rights reserved.
// </copyright>

using Zearain.AoC23.Domain.Common;

namespace Zearain.AoC23.Domain.AdventDayAggregate.ValueObjects;

/// <summary>
/// Represents the input of an advent day.
/// </summary>
public class DayInput : ValueObject
{
    private DayInput()
    {
    }

    private DayInput(string rawInput)
    {
        this.RawInput = rawInput;
    }

    /// <summary>
    /// Gets the raw text input.
    /// </summary>
    public string RawInput { get; private set; } = string.Empty;

    /// <summary>
    /// Gets the lines of the input.
    /// </summary>
    public string[] Lines => SplitLines(this.GetEnvionmentSafeInput());

    /// <summary>
    /// Gets the sections of the input.
    /// </summary>
    public string[] Sections => string.IsNullOrWhiteSpace(this.RawInput) ? Array.Empty<string>() : this.GetEnvionmentSafeInput().Split($"{Environment.NewLine}{Environment.NewLine}");

    /// <summary>
    /// Gets the sections of the input, split into lines.
    /// </summary>
    public string[][] SectionLines => this.Sections.Select(SplitLines).ToArray();

    /// <summary>
    /// Creates a new <see cref="DayInput"/> from the given input.
    /// </summary>
    /// <param name="input">The input.</param>
    /// <returns>A new instance of <see cref="DayInput"/>.</returns>
    public static DayInput Create(string input)
    {
        return new DayInput(input.TrimEnd(Environment.NewLine.ToCharArray()));
    }

    /// <summary>
    /// Updates the <see cref="DayInput"/> with the given input.
    /// </summary>
    /// <param name="input">The input.</param>
    public void Update(string input)
    {
        this.RawInput = input.TrimEnd(Environment.NewLine.ToCharArray());
    }

    /// <inheritdoc/>
    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return this.RawInput;
    }

    private static string[] SplitLines(string input) => string.IsNullOrWhiteSpace(input) ? Array.Empty<string>() : input.Split(Environment.NewLine);

    private string GetEnvionmentSafeInput()
    {
        return Environment.NewLine == "\r\n" ? this.RawInput : this.RawInput.Replace("\r\n", Environment.NewLine);
    }
}