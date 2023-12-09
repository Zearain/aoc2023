// <copyright file="CamelCardHand.cs" company="Zearain">
// Copyright (c) Zearain. All rights reserved.
// </copyright>

namespace Zearain.AoC23.Application.AdventDays.Services.CamelCards;

public record CamelCardHand(CamelCard[] Cards, int Bid) : IComparable
{
    public static bool operator >(CamelCardHand left, CamelCardHand right)
    {
        return left.CompareTo(right) > 0;
    }

    public static bool operator <(CamelCardHand left, CamelCardHand right)
    {
        return left.CompareTo(right) < 0;
    }

    public virtual CamelCardHandType GetHandType()
    {
        var groupedCards = this.Cards.GroupBy(c => c.Label).OrderByDescending(g => g.Count()).ToArray();

        if (groupedCards.Length == 5)
        {
            return CamelCardHandType.HighCard;
        }

        if (groupedCards.Length == 4)
        {
            return CamelCardHandType.Pair;
        }

        if (groupedCards.Length == 3)
        {
            return groupedCards[0].Count() == 3 ? CamelCardHandType.ThreeOfAKind : CamelCardHandType.TwoPair;
        }

        if (groupedCards.Length == 2)
        {
            return groupedCards[0].Count() == 4 ? CamelCardHandType.FourOfAKind : CamelCardHandType.FullHouse;
        }

        return CamelCardHandType.FiveOfAKind;
    }

    public int CompareTo(object? obj)
    {
        if (obj is CamelCardHand other)
        {
            var thisHandType = this.GetHandType();
            var otherHandType = other.GetHandType();
            if (thisHandType != otherHandType)
            {
                return thisHandType.CompareTo(otherHandType);
            }

            var i = 0;
            while (i < this.Cards.Length)
            {
                var thisCard = this.Cards[i];
                var otherCard = other.Cards[i];
                if (thisCard.Value != otherCard.Value)
                {
                    return thisCard.Value.CompareTo(otherCard.Value);
                }

                i++;
            }
        }

        return 0;
    }
}

public enum CamelCardHandType
{
    HighCard,
    Pair,
    TwoPair,
    ThreeOfAKind,
    FullHouse,
    FourOfAKind,
    FiveOfAKind,
}