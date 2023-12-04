// <copyright file="ScratchcardReader.cs" company="Zearain">
// Copyright (c) Zearain. All rights reserved.
// </copyright>

using System.Globalization;

using Microsoft.Extensions.Logging;

using Zearain.AoC23.Domain.AdventDayAggregate.ValueObjects;

namespace Zearain.AoC23.Application.AdventDays.Services.Scratchcards;

/// <summary>
/// Reads scratchcards from the input.
/// </summary>
/// <remarks>
/// Day 4 of Advent of Code 2023: https://adventofcode.com/2023/day/4.
/// </remarks>
public sealed class ScratchcardReader
{
    private readonly ILogger logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="ScratchcardReader"/> class.
    /// </summary>
    /// <param name="logger">The logger.</param>
    public ScratchcardReader(ILogger<ScratchcardReader> logger)
    {
        this.logger = logger;
    }

    /// <summary>
    /// Reads the scratchcards from the input.
    /// </summary>
    /// <param name="input">The input.</param>
    /// <returns>The scratchcards.</returns>
    public IEnumerable<Scratchcard> ReadScratchcards(DayInput input)
    {
        var scratchcards = input.Lines.Select(ReadScratchcard).ToArray();
        this.logger.ReadScratchcards(scratchcards.Length);
        return scratchcards;
    }

    /// <summary>
    /// Processes the card winnings by copying cards based on the number of matches.
    /// </summary>
    /// <param name="scratchcards">The scratchcards.</param>
    /// <returns>The processed scratchcards.</returns>
#pragma warning disable CA1822 // Mark members as static
    public IEnumerable<Scratchcard> ProcessCardWinnings(IEnumerable<Scratchcard> scratchcards)
#pragma warning restore CA1822 // Mark members as static
    {
        var originalCards = scratchcards.ToArray();
        var cards = scratchcards.ToList();
        var i = 0;
        while (i < cards.Count)
        {
            var card = cards[i];
            for (var m = 0; m < card.Matches; m++)
            {
                if (card.CardNumber + m < originalCards.Length)
                {
                    cards.Insert(i + 1 + m, originalCards[card.CardNumber + m]);
                }
            }

            i++;
        }

        return cards;
    }

    private static Scratchcard ReadScratchcard(string input)
    {
        // Split by : to get the card number and the scratchcard
        var split = input.Split(':');
        var cardNumber = int.Parse(split[0].Split(' ')[^1].Trim(), CultureInfo.InvariantCulture);
        var scratchcard = split[1].Trim().Split('|');
        var winningNumbers = scratchcard[0].Trim().Split(' ').Where(s => string.IsNullOrWhiteSpace(s) == false).Select(int.Parse).ToArray();
        var scratchcardNumbers = scratchcard[1].Trim().Split(' ').Where(s => string.IsNullOrWhiteSpace(s) == false).Select(int.Parse).ToArray();

        return new Scratchcard(cardNumber, winningNumbers, scratchcardNumbers);
    }
}