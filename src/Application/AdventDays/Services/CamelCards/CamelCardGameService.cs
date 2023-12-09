// <copyright file="CamelCardGameService.cs" company="Zearain">
// Copyright (c) Zearain. All rights reserved.
// </copyright>

using System.Globalization;

using Zearain.AoC23.Domain.AdventDayAggregate.ValueObjects;

namespace Zearain.AoC23.Application.AdventDays.Services.CamelCards;

public class CamelCardGameService
{
    public static IEnumerable<CamelCardHand> ReadHands(DayInput input)
    {
        var lines = input.Lines;
        foreach (var line in lines)
        {
            var cardBid = line.Split(' ');
            var cards = cardBid[0];
            var bid = int.Parse(cardBid[1], CultureInfo.InvariantCulture);
            yield return new CamelCardHand(cards.Select(c => new CamelCard(c)).ToArray(), bid);
        }
    }

    public static IEnumerable<CamelJokerCardHand> ReadJokerHands(DayInput input)
    {
        var lines = input.Lines;
        foreach (var line in lines)
        {
            var cardBid = line.Split(' ');
            var cards = cardBid[0];
            var bid = int.Parse(cardBid[1], CultureInfo.InvariantCulture);
            yield return new CamelJokerCardHand(cards.Select(c => new CamelJokerCard(c)).ToArray(), bid);
        }
    }

    public static IEnumerable<long> GetHandWinnings(IEnumerable<CamelCardHand> hands)
    {
        var orderedHands = hands.Order().ToArray();

        for (var i = 0; i < orderedHands.Length; i++)
        {
            var hand = orderedHands[i];
            var winnings = hand.Bid * (i + 1);

            if (winnings == 0)
            {
                throw new InvalidOperationException("Winnings cannot be zero.");
            }

            yield return winnings;
        }
    }

    public static long GetTotalWinnings(IEnumerable<CamelCardHand> hands)
    {
        return GetHandWinnings(hands).Sum();
    }
}
