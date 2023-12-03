// <copyright file="GondolaEngineSchematicReaderTests.cs" company="Zearain">
// Copyright (c) Zearain. All rights reserved.
// </copyright>

using Microsoft.Extensions.Logging;

using Zearain.AoC23.Application.AdventDays.Services.GondolaEngineSchematics;

namespace Zearain.AoC23.Application.Tests.Services;

[TestFixture(Description = "Tests for GondolaEngineSchematicReader.")]
public class GondolaEngineSchematicReaderTests
{
    private const string RawTestInput = @"467..114..
...*......
..35..633.
......#...
617*......
.....+.58.
..592.....
......755.
...$.*....
.664.598..";

    private static readonly DayInput TestInput = DayInput.Create(RawTestInput);

    [Test(Description = "Read should return the correct schematic.")]
    public void Read_ShouldReturnSchematic()
    {
        // Arrange
        var expectedParts = new List<GondolaEngineSchematicPart>
        {
            new(467, 0, new[] { 0, 1, 2 }),
            new(35, 2, new[] { 2, 3 }),
            new(633, 2, new[] { 6, 7, 8 }),
            new(617, 4, new[] { 0, 1, 2 }),
            new(592, 6, new[] { 2, 3, 4 }),
            new(755, 7, new[] { 6, 7, 8 }),
            new(664, 9, new[] { 1, 2, 3 }),
            new(598, 9, new[] { 5, 6, 7 }),
        };

        var expectedSymbols = new List<GondolaEngineSchematicSymbol>
        {
            new('*', 1, 3),
            new('#', 3, 6),
            new('*', 4, 3),
            new('+', 5, 5),
            new('$', 8, 3),
            new('*', 8, 5),
        };

        var loggerMock = new Mock<ILogger<GondolaEngineSchematicReader>>();
        var reader = new GondolaEngineSchematicReader(loggerMock.Object);

        // Act
        var schematic = reader.Read(TestInput);

        // Assert
        schematic.Schematic.GetLength(0).Should().Be(10);
        schematic.Schematic.GetLength(1).Should().Be(10);
        schematic.PartNumbers.Should().BeEquivalentTo(expectedParts);
        schematic.PartNumbers.Sum(p => p.Number).Should().Be(4361);
        schematic.Symbols.Should().BeEquivalentTo(expectedSymbols);
    }

    [Test(Description = "GetGearRatios should return the correct gear ratios.")]
    public void GetGearRatios_ShouldReturnExpectedGearRatios()
    {
        // Arrange
        var expectedGearRatios = new[] { 16345, 451490 };

        var loggerMock = new Mock<ILogger<GondolaEngineSchematicReader>>();
        var reader = new GondolaEngineSchematicReader(loggerMock.Object);

        // Act
        var schematic = reader.Read(TestInput);

        // Assert
        schematic.GetGearRatios().Should().BeEquivalentTo(expectedGearRatios);
    }
}