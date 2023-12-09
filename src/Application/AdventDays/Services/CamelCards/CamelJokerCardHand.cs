// <copyright file="CamelJokerCardHand.cs" company="Zearain">
// Copyright (c) Zearain. All rights reserved.
// </copyright>

namespace Zearain.AoC23.Application.AdventDays.Services.CamelCards;

public record CamelJokerCardHand(CamelCard[] Cards, int Bid) : CamelCardHand(Cards, Bid)
{
    public override CamelCardHandType GetHandType()
    {
        var groupedCards = this.Cards.Cast<CamelJokerCard>().GroupBy(c => c.Label).OrderByDescending(g => g.Count()).ToArray();

        if (groupedCards.Length == 5)
        {
            return groupedCards.Any(g => g.Any(c => c.IsJoker)) ? CamelCardHandType.Pair : CamelCardHandType.HighCard;
        }

        if (groupedCards.Length == 4)
        {
            return groupedCards.Any(g => g.Any(c => c.IsJoker)) ? CamelCardHandType.ThreeOfAKind : CamelCardHandType.Pair;
        }

        if (groupedCards.Length == 3)
        {
            // If the first group has a length of 3, and there are jokers in any of the groups, then it's four of a kind.
            var jokersInGroups = groupedCards.FirstOrDefault(g => g.Any(c => c.IsJoker))?.Count() ?? 0;
            if (groupedCards[0].Count() == 3 && jokersInGroups > 0)
            {
                return CamelCardHandType.FourOfAKind;
            }

            if (jokersInGroups > 1)
            {
                return CamelCardHandType.FourOfAKind;
            }

            // If there are jokers in any of the groups, then it's a full house.
            return jokersInGroups > 0
                ? CamelCardHandType.FullHouse
                : groupedCards[0].Count() == 3 ? CamelCardHandType.ThreeOfAKind : CamelCardHandType.TwoPair;
        }

        if (groupedCards.Length == 2)
        {
            return groupedCards.Any(g => g.Any(c => c.IsJoker))
                ? CamelCardHandType.FiveOfAKind
                : groupedCards[0].Count() == 4 ? CamelCardHandType.FourOfAKind : CamelCardHandType.FullHouse;
        }

        return CamelCardHandType.FiveOfAKind;
    }
}