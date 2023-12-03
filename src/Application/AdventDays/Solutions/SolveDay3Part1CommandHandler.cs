// <copyright file="SolveDay3Part1CommandHandler.cs" company="Zearain">
// Copyright (c) Zearain. All rights reserved.
// </copyright>

using System.Globalization;

using ErrorOr;

using MediatR;

using Zearain.AoC23.Application.AdventDays.Services;
using Zearain.AoC23.Domain.AdventDayAggregate.Entities;

namespace Zearain.AoC23.Application.AdventDays.Solutions;

/// <summary>
/// Represents a handler for <see cref="SolveDay3Part1Command"/>.
/// </summary>
public class SolveDay3Part1CommandHandler : IRequestHandler<SolveDay3Part1Command, ErrorOr<PartSolution>>
{
    private readonly GondolaEngineSchematicReader gondolaEngineSchematicReader;

    /// <summary>
    /// Initializes a new instance of the <see cref="SolveDay3Part1CommandHandler"/> class.
    /// </summary>
    /// <param name="gondolaEngineSchematicReader">The reader for Gondola Engine Schematics.</param>
    public SolveDay3Part1CommandHandler(GondolaEngineSchematicReader gondolaEngineSchematicReader)
    {
        this.gondolaEngineSchematicReader = gondolaEngineSchematicReader;
    }

    /// <inheritdoc />
    public Task<ErrorOr<PartSolution>> Handle(SolveDay3Part1Command request, CancellationToken cancellationToken)
    {
        var input = request.Input;

        var schematic = this.gondolaEngineSchematicReader.Read(input);

        var sum = schematic.PartNumbers.Sum(p => p.Number);

        var solution = PartSolution.Create(request.PartNumber, sum.ToString(CultureInfo.InvariantCulture));
        return Task.FromResult(solution);
    }
}