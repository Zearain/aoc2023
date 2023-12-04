// <copyright file="Scratchcard.cs" company="Zearain">
// Copyright (c) Zearain. All rights reserved.
// </copyright>

namespace Zearain.AoC23.Application.AdventDays.Services.Scratchcards;

/// <summary>
/// Represents a scratchcard.
/// </summary>
/// <param name="CardNumber">The card number.</param>
/// <param name="WinningNumbers">The winning numbers.</param>
/// <param name="ScratchcardNumbers">The scratchcard numbers.</param>
public record Scratchcard(int CardNumber, int[] WinningNumbers, int[] ScratchcardNumbers)
{
    /// <summary>
    /// Gets the value of the scratchcard.
    /// </summary>
    public int Value => CalculateValue(this.Matches);

    /// <summary>
    /// Gets the number of matches.
    /// </summary>
    public int Matches => this.ScratchcardNumbers.Count(this.WinningNumbers.Contains);

    private static int CalculateValue(int matches)
    {
        return matches switch
        {
            0 => 0,
            _ => Exponential(matches),
        };
    }

    private static int Exponential(int n)
    {
        return n switch
        {
            0 => 1,
            1 => 1,
            _ => 2 * Exponential(n - 1),
        };
    }
}