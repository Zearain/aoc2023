// <copyright file="SeedAlmanacReaderTests.cs" company="Zearain">
// Copyright (c) Zearain. All rights reserved.
// </copyright>

using Microsoft.Extensions.Logging;

using Zearain.AoC23.Application.AdventDays.Services.SeedMapping;

namespace Zearain.AoC23.Application.Tests.Services;

[TestFixture]
public class SeedAlmanacReaderTests
{
    private const string RawInput = @"seeds: 79 14 55 13

seed-to-soil map:
50 98 2
52 50 48

soil-to-fertilizer map:
0 15 37
37 52 2
39 0 15

fertilizer-to-water map:
49 53 8
0 11 42
42 0 7
57 7 4

water-to-light map:
88 18 7
18 25 70

light-to-temperature map:
45 77 23
81 45 19
68 64 13

temperature-to-humidity map:
0 69 1
1 0 69

humidity-to-location map:
60 56 37
56 93 4";

    private static readonly DayInput TestInput = DayInput.Create(RawInput);

    private SeedAlmanacReader reader;

    [SetUp]
    public void SetUp()
    {
        this.reader = new SeedAlmanacReader(Mock.Of<ILogger<SeedAlmanacReader>>());
    }

    [Test(Description = "Tests that the Read method returns the expected seed type and values.")]
    public void Read_ReturnsExpectedSeedTypeAndValues()
    {
        // Arrange
        var expectedSeedType = "seed";
        var expectedSeedValues = new[] { 79, 14, 55, 13 };

        // Act
        var result = this.reader.Read(TestInput);

        // Assert
        result.SeedType.Should().Be(expectedSeedType);
        result.SeedValues.Should().BeEquivalentTo(expectedSeedValues);
    }

    [TestCaseSource(nameof(ReadMapTestCases))]
    public void ReadMap_ReturnsExpectedMap(string[] lines, AlmanacMap expectedMap)
    {
        // Act
        var result = this.reader.ReadMap(lines);

        // Assert
        result.Should().BeEquivalentTo(expectedMap);
    }

    [TestCaseSource(nameof(AlmanacMapGetMappedValueTestCases))]
    public int AlmanacMap_GetMappedValue_ReturnsExpectedValue(AlmanacMap map, int value)
    {
        // Act
        var result = map.GetMappedValue(value);

        // Assert
        return result;
    }

    [Test(Description = "Tests that the GetSeedValueMappings method returns the expected mappings.")]
    public void SeedAlmanac_GetSeedValueMappings_ReturnsExpectedMappings()
    {
        // Arrange
        var expectedMappings = new MappingResult[][]
        {
            [
                new MappingResult("seed", 79), new MappingResult("soil", 81), new MappingResult("fertilizer", 81),
                new MappingResult("water", 81), new MappingResult("light", 74), new MappingResult("temperature", 78),
                new MappingResult("humidity", 78), new MappingResult("location", 82)
            ],
            [
                new MappingResult("seed", 14), new MappingResult("soil", 14), new MappingResult("fertilizer", 53),
                new MappingResult("water", 49), new MappingResult("light", 42), new MappingResult("temperature", 42),
                new MappingResult("humidity", 43), new MappingResult("location", 43)
            ],
            [
                new MappingResult("seed", 55), new MappingResult("soil", 57), new MappingResult("fertilizer", 57),
                new MappingResult("water", 53), new MappingResult("light", 46), new MappingResult("temperature", 82),
                new MappingResult("humidity", 82), new MappingResult("location", 86)
            ],
            [
                new MappingResult("seed", 13), new MappingResult("soil", 13), new MappingResult("fertilizer", 52),
                new MappingResult("water", 41), new MappingResult("light", 34), new MappingResult("temperature", 34),
                new MappingResult("humidity", 35), new MappingResult("location", 35)
            ],
        };

        // Act
        var seedAlmanac = this.reader.Read(TestInput);
        var actualMappings = seedAlmanac.GetSeedValueMappings();

        // Assert
        actualMappings.Should().BeEquivalentTo(expectedMappings);
    }

    private static IEnumerable<TestCaseData> ReadMapTestCases()
    {
        yield return new TestCaseData(
            new[]
            {
                "seed-to-soil map:",
                "50 98 2",
                "52 50 48",
            },
            new AlmanacMap("seed", "soil", new[] { new MapRange(98, 50, 2), new MapRange(50, 52, 48) }));

        yield return new TestCaseData(
            new[]
            {
                "soil-to-fertilizer map:",
                "0 15 37",
                "37 52 2",
                "39 0 15",
            },
            new AlmanacMap("soil", "fertilizer", new[] { new MapRange(15, 0, 37), new MapRange(52, 37, 2), new MapRange(0, 39, 15) }));

        yield return new TestCaseData(
            new[]
            {
                "fertilizer-to-water map:",
                "49 53 8",
                "0 11 42",
                "42 0 7",
                "57 7 4",
            },
            new AlmanacMap("fertilizer", "water", new[] { new MapRange(53, 49, 8), new MapRange(11, 0, 42), new MapRange(0, 42, 7), new MapRange(7, 57, 4) }));

        yield return new TestCaseData(
            new[]
            {
                "water-to-light map:",
                "88 18 7",
                "18 25 70",
            },
            new AlmanacMap("water", "light", new[] { new MapRange(18, 88, 7), new MapRange(25, 18, 70) }));

        yield return new TestCaseData(
            new[]
            {
                "light-to-temperature map:",
                "45 77 23",
                "81 45 19",
                "68 64 13",
            },
            new AlmanacMap("light", "temperature", new[] { new MapRange(77, 45, 23), new MapRange(45, 81, 19), new MapRange(64, 68, 13) }));

        yield return new TestCaseData(
            new[]
            {
                "temperature-to-humidity map:",
                "0 69 1",
                "1 0 69",
            },
            new AlmanacMap("temperature", "humidity", new[] { new MapRange(69, 0, 1), new MapRange(0, 1, 69) }));

        yield return new TestCaseData(
            new[]
            {
                "humidity-to-location map:",
                "60 56 37",
                "56 93 4",
            },
            new AlmanacMap("humidity", "location", new[] { new MapRange(56, 60, 37), new MapRange(93, 56, 4) }));
    }

    private static IEnumerable<TestCaseData> AlmanacMapGetMappedValueTestCases()
    {
        yield return new TestCaseData(
            new AlmanacMap("seed", "soil", new[] { new MapRange(98, 50, 2), new MapRange(50, 52, 48) }), 79).Returns(81);
        yield return new TestCaseData(
            new AlmanacMap("seed", "soil", new[] { new MapRange(98, 50, 2), new MapRange(50, 52, 48) }), 14).Returns(14);
        yield return new TestCaseData(
            new AlmanacMap("seed", "soil", new[] { new MapRange(98, 50, 2), new MapRange(50, 52, 48) }), 55).Returns(57);
        yield return new TestCaseData(
            new AlmanacMap("seed", "soil", new[] { new MapRange(98, 50, 2), new MapRange(50, 52, 48) }), 13).Returns(13);
    }
}