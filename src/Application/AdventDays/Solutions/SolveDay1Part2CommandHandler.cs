// <copyright file="SolveDay1Part2CommandHandler.cs" company="Zearain">
// Copyright (c) Zearain. All rights reserved.
// </copyright>

using System.Globalization;

using ErrorOr;

using MediatR;

using Zearain.AoC23.Domain.AdventDayAggregate.Entities;
using Zearain.AoC23.Domain.AdventDayAggregate.ValueObjects;

namespace Zearain.AoC23.Application.AdventDays.Solutions;

/// <summary>
/// Represents a handler for <see cref="SolveDay1Part2Command"/>.
/// </summary>
public class SolveDay1Part2CommandHandler : IRequestHandler<SolveDay1Part2Command, ErrorOr<PartSolution>>
{
    private readonly IDictionary<string, string> numberReplacements = new Dictionary<string, string>
    {
        { "one", "o1ne" },
        { "two", "t2wo" },
        { "three", "t3hree" },
        { "four", "f4our" },
        { "five", "f5ive" },
        { "six", "s6ix" },
        { "seven", "s7even" },
        { "eight", "e8ight" },
        { "nine", "n9ine" },
        { "zero", "z0ero" },
    };

    /// <inheritdoc/>
    public Task<ErrorOr<PartSolution>> Handle(SolveDay1Part2Command request, CancellationToken cancellationToken)
    {
        var input = request.Input;

        var newLines = input.Lines.Select(this.ReplaceNumbers);
        var newRawInput = string.Join(Environment.NewLine, newLines);

        var newInput = DayInput.Create(newRawInput);

        var sum = SolveDay1Part1CommandHandler.SumForInput(newInput);
        var solution = PartSolution.Create(request.PartNumber, sum.ToString(CultureInfo.InvariantCulture));
        return Task.FromResult(solution);
    }

    private string ReplaceNumbers(string line)
    {
        var newLine = line;
        foreach (var (key, value) in this.numberReplacements)
        {
            newLine = newLine.Replace(key, value);
        }

        return newLine;
    }
}