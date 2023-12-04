// <copyright file="SolveDay4Part1CommandHandler.cs" company="Zearain">
// Copyright (c) Zearain. All rights reserved.
// </copyright>

using System.Globalization;

using ErrorOr;

using MediatR;

using Zearain.AoC23.Application.AdventDays.Services.Scratchcards;
using Zearain.AoC23.Domain.AdventDayAggregate.Entities;

namespace Zearain.AoC23.Application.AdventDays.Solutions;

/// <summary>
/// Represents a handler for <see cref="SolveDay4Part1Command"/>.
/// </summary>
public class SolveDay4Part1CommandHandler : IRequestHandler<SolveDay4Part1Command, ErrorOr<PartSolution>>
{
    private readonly ScratchcardReader scratchcardReader;

    /// <summary>
    /// Initializes a new instance of the <see cref="SolveDay4Part1CommandHandler"/> class.
    /// </summary>
    /// <param name="scratchcardReader">The scratchcard reader.</param>
    public SolveDay4Part1CommandHandler(ScratchcardReader scratchcardReader)
    {
        this.scratchcardReader = scratchcardReader;
    }

    /// <inheritdoc />
    public Task<ErrorOr<PartSolution>> Handle(SolveDay4Part1Command request, CancellationToken cancellationToken)
    {
        var input = request.Input;

        var scratchcards = this.scratchcardReader.ReadScratchcards(input).ToArray();

        var sum = scratchcards.Sum(s => s.Value);

        var solution = PartSolution.Create(request.PartNumber, sum.ToString(CultureInfo.InvariantCulture));
        return Task.FromResult(solution);
    }
}