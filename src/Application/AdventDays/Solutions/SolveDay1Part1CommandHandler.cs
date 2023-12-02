// <copyright file="SolveDay1Part1CommandHandler.cs" company="Zearain">
// Copyright (c) Zearain. All rights reserved.
// </copyright>

using System.Globalization;

using ErrorOr;

using MediatR;

using Zearain.AoC23.Domain.AdventDayAggregate.Entities;
using Zearain.AoC23.Domain.AdventDayAggregate.ValueObjects;

namespace Zearain.AoC23.Application.AdventDays.Solutions;

/// <summary>
/// Represents a handler for <see cref="SolveDay1Part1Command"/>.
/// </summary>
public class SolveDay1Part1CommandHandler : IRequestHandler<SolveDay1Part1Command, ErrorOr<PartSolution>>
{
    /// <summary>
    /// Sums the first and last digits of each line in the input.
    /// </summary>
    /// <param name="input">The input.</param>
    /// <returns>The sum of the first and last digits of each line in the input.</returns>
    public static int SumForInput(DayInput input)
    {
        var sum = input.Lines
            .Where(line => !string.IsNullOrWhiteSpace(line))
            .Select(LineNumber)
            .Sum();

        return sum;
    }

    /// <inheritdoc/>
    public Task<ErrorOr<PartSolution>> Handle(SolveDay1Part1Command request, CancellationToken cancellationToken)
    {
        var input = request.Input;

        var sum = SumForInput(input);

        var solution = PartSolution.Create(request.PartNumber, sum.ToString(CultureInfo.InvariantCulture));
        return Task.FromResult(solution);
    }

    /// <summary>
    /// Gets the line number from the line by parsing the first and last digits.
    /// </summary>
    /// <param name="line">The line to parse.</param>
    /// <returns>The line number.</returns>
    private static int LineNumber(string line)
    {
        var lineDigits = line.Where(c => char.IsDigit(c)).ToArray();
        var lineDigitsString = new string(new[] { lineDigits.First(), lineDigits.Last() });
        return int.Parse(lineDigitsString, CultureInfo.InvariantCulture);
    }
}