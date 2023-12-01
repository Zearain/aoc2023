// <copyright file="SolveDay1Part1CommandHandler.cs" company="Zearain">
// Copyright (c) Zearain. All rights reserved.
// </copyright>

using System.Globalization;

using ErrorOr;

using MediatR;

using Zearain.AoC23.Domain.AdventDayAggregate.Entities;

namespace Zearain.AoC23.Application;

/// <summary>
/// Represents a handler for <see cref="SolveDay1Part1Command"/>.
/// </summary>
public class SolveDay1Part1CommandHandler : IRequestHandler<SolveDay1Part1Command, ErrorOr<PartSolution>>
{
    /// <inheritdoc/>
    public Task<ErrorOr<PartSolution>> Handle(SolveDay1Part1Command request, CancellationToken cancellationToken)
    {
        var input = request.Input;

        var sum = input.Lines
            .Select(LineNumber)
            .Sum();

        var solution = PartSolution.Create(request.PartNumber, sum.ToString(CultureInfo.InvariantCulture));
        return Task.FromResult(solution);
    }

    private static int LineNumber(string line)
    {
        var lineDigits = line.Where(c => char.IsDigit(c)).ToArray();
        var lineDigitsString = new string(new[] { lineDigits[0], lineDigits[^1] });
        return int.Parse(lineDigitsString, CultureInfo.InvariantCulture);
    }
}