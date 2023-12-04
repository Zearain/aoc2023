// <copyright file="ScratchcardReaderTests.cs" company="Zearain">
// Copyright (c) Zearain. All rights reserved.
// </copyright>

using Microsoft.Extensions.Logging;

using Zearain.AoC23.Application.AdventDays.Services.Scratchcards;

namespace Zearain.AoC23.Application.Tests;

public class ScratchcardReaderTests
{
    private const string RawTestInput = @"Card 1: 41 48 83 86 17 | 83 86  6 31 17  9 48 53
Card 2: 13 32 20 16 61 | 61 30 68 82 17 32 24 19
Card 3:  1 21 53 59 44 | 69 82 63 72 16 21 14  1
Card 4: 41 92 73 84 69 | 59 84 76 51 58  5 54 83
Card 5: 87 83 26 28 32 | 88 30 70 12 93 22 82 36
Card 6: 31 18 13 56 72 | 74 77 10 23 35 67 36 11";

    private static readonly DayInput TestInput = DayInput.Create(RawTestInput);

    private readonly Mock<ILogger<ScratchcardReader>> loggerMock = new Mock<ILogger<ScratchcardReader>>();

    [TestCaseSource(nameof(ScratchcardValueTestCases))]
    public void ScratchcardReader_ScratchcardHasCorrectMatchesAndValue(Scratchcard scratchcard, int expectedMatches, int expectedValue)
    {
        // Assert
        scratchcard.Matches.Should().Be(expectedMatches);
        scratchcard.Value.Should().Be(expectedValue);
    }

    [Test(Description = "ProcessCardWinnings should return the correct number of cards")]
    public void ProcessCardWinnings_ReturnsCorrectNumberOfCards()
    {
        // Arrange
        var scratchcardReader = new ScratchcardReader(this.loggerMock.Object);
        var scratchcards = scratchcardReader.ReadScratchcards(TestInput);

        // Act
        var processedScratchcards = scratchcardReader.ProcessCardWinnings(scratchcards);

        // Assert
        processedScratchcards.Should().HaveCount(30);
    }

    private static IEnumerable<TestCaseData> ScratchcardValueTestCases()
    {
        yield return new TestCaseData(new Scratchcard(1, new[] { 41, 48, 83, 86, 17 }, new[] { 83, 86, 6, 31, 17, 9, 48, 53 }), 4, 8);
        yield return new TestCaseData(new Scratchcard(2, new[] { 13, 32, 20, 16, 61 }, new[] { 61, 30, 68, 82, 17, 32, 24, 19 }), 2, 2);
        yield return new TestCaseData(new Scratchcard(3, new[] { 1, 21, 53, 59, 44 }, new[] { 69, 82, 63, 72, 16, 21, 14, 1 }), 2, 2);
        yield return new TestCaseData(new Scratchcard(4, new[] { 41, 92, 73, 84, 69 }, new[] { 59, 84, 76, 51, 58, 5, 54, 83 }), 1, 1);
        yield return new TestCaseData(new Scratchcard(5, new[] { 87, 83, 26, 28, 32 }, new[] { 88, 30, 70, 12, 93, 22, 82, 36 }), 0, 0);
        yield return new TestCaseData(new Scratchcard(6, new[] { 31, 18, 13, 56, 72 }, new[] { 74, 77, 10, 23, 35, 67, 36, 11 }), 0, 0);
    }
}