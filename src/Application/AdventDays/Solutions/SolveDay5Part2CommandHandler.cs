// <copyright file="SolveDay5Part2CommandHandler.cs" company="Zearain">
// Copyright (c) Zearain. All rights reserved.
// </copyright>

using System.Globalization;

using ErrorOr;

using MediatR;

using Zearain.AoC23.Application.AdventDays.Services.SeedMapping;
using Zearain.AoC23.Domain.AdventDayAggregate.Entities;

namespace Zearain.AoC23.Application.AdventDays.Solutions;

/// <summary>
/// Represents a handler for <see cref="SolveDay5Part2Command"/>.
/// </summary>
public class SolveDay5Part2CommandHandler : IRequestHandler<SolveDay5Part2Command, ErrorOr<PartSolution>>
{
    private readonly SeedAlmanacReader seedAlmanacReader;

    /// <summary>
    /// Initializes a new instance of the <see cref="SolveDay5Part2CommandHandler"/> class.
    /// </summary>
    /// <param name="seedAlmanacReader">The seed almanac reader.</param>
    public SolveDay5Part2CommandHandler(SeedAlmanacReader seedAlmanacReader)
    {
        this.seedAlmanacReader = seedAlmanacReader;
    }

    /// <inheritdoc />
    public Task<ErrorOr<PartSolution>> Handle(SolveDay5Part2Command request, CancellationToken cancellationToken)
    {
        var input = request.Input;

        var almanac = this.seedAlmanacReader.Read(input);
        var lowestMappedValue = almanac.GetLowestFinalMappedValueForSeedRanges();

        var solution = PartSolution.Create(request.PartNumber, lowestMappedValue.ToString(CultureInfo.InvariantCulture));
        return Task.FromResult(solution);
    }
}