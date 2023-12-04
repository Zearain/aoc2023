// <copyright file="SolveDay4Part2CommandHandler.cs" company="Zearain">
// Copyright (c) Zearain. All rights reserved.
// </copyright>

using System.Globalization;

using ErrorOr;

using MediatR;

using Zearain.AoC23.Application.AdventDays.Services.Scratchcards;
using Zearain.AoC23.Domain.AdventDayAggregate.Entities;

namespace Zearain.AoC23.Application.AdventDays.Solutions;

/// <summary>
/// Represents a handler for <see cref="SolveDay4Part2Command"/>.
/// </summary>
public class SolveDay4Part2CommandHandler : IRequestHandler<SolveDay4Part2Command, ErrorOr<PartSolution>>
{
    private readonly ScratchcardReader scratchcardReader;

    /// <summary>
    /// Initializes a new instance of the <see cref="SolveDay4Part2CommandHandler"/> class.
    /// </summary>
    /// <param name="scratchcardReader">The scratchcard reader.</param>
    public SolveDay4Part2CommandHandler(ScratchcardReader scratchcardReader)
    {
        this.scratchcardReader = scratchcardReader;
    }

    /// <inheritdoc />
    public Task<ErrorOr<PartSolution>> Handle(SolveDay4Part2Command request, CancellationToken cancellationToken)
    {
        var input = request.Input;

        var scratchcards = this.scratchcardReader.ReadScratchcards(input).ToArray();
        var processedScratchcards = this.scratchcardReader.ProcessCardWinnings(scratchcards).ToArray();

        var solution = PartSolution.Create(request.PartNumber, processedScratchcards.Length.ToString(CultureInfo.InvariantCulture));
        return Task.FromResult(solution);
    }
}