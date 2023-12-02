// <copyright file="SolveDay2Part1CommandHandler.cs" company="Zearain">
// Copyright (c) Zearain. All rights reserved.
// </copyright>

using System.Globalization;

using ErrorOr;

using MediatR;

using Zearain.AoC23.Application.AdventDays.Services;
using Zearain.AoC23.Domain.AdventDayAggregate.Entities;

namespace Zearain.AoC23.Application.AdventDays.Solutions;

/// <summary>
/// Represents a handler for <see cref="SolveDay2Part1Command"/>.
/// </summary>
public class SolveDay2Part1CommandHandler : IRequestHandler<SolveDay2Part1Command, ErrorOr<PartSolution>>
{
    /// <inheritdoc />
    public Task<ErrorOr<PartSolution>> Handle(SolveDay2Part1Command request, CancellationToken cancellationToken)
    {
        var input = request.Input;

        var sum = ColoredCubeGameService.SumPossibleGameIds(input, 12, 13, 14);

        var solution = PartSolution.Create(request.PartNumber, sum.ToString(CultureInfo.InvariantCulture));
        return Task.FromResult(solution);
    }
}
