// <copyright file="SolveDay5Part1CommandHandler.cs" company="Zearain">
// Copyright (c) Zearain. All rights reserved.
// </copyright>

using System.Globalization;

using ErrorOr;

using MediatR;

using Zearain.AoC23.Application.AdventDays.Services.SeedMapping;
using Zearain.AoC23.Domain.AdventDayAggregate.Entities;

namespace Zearain.AoC23.Application.AdventDays.Solutions;

/// <summary>
/// Represents a handler for <see cref="SolveDay5Part1Command"/>.
/// </summary>
public class SolveDay5Part1CommandHandler : IRequestHandler<SolveDay5Part1Command, ErrorOr<PartSolution>>
{
    private readonly SeedAlmanacReader seedAlmanacReader;

    /// <summary>
    /// Initializes a new instance of the <see cref="SolveDay5Part1CommandHandler"/> class.
    /// </summary>
    /// <param name="seedAlmanacReader">The seed almanac reader.</param>
    public SolveDay5Part1CommandHandler(SeedAlmanacReader seedAlmanacReader)
    {
        this.seedAlmanacReader = seedAlmanacReader;
    }

    /// <inheritdoc />
    public Task<ErrorOr<PartSolution>> Handle(SolveDay5Part1Command request, CancellationToken cancellationToken)
    {
        var input = request.Input;

        var seedAlmanac = this.seedAlmanacReader.Read(input);
        var lowestMappedValue = SeedAlmanacReader.GetLowestMappedValue(seedAlmanac);

        var solution = PartSolution.Create(request.PartNumber, lowestMappedValue.ToString(CultureInfo.InvariantCulture));
        return Task.FromResult(solution);
    }
}