// <copyright file="SolveDay6Part1CommandHandler.cs" company="Zearain">
// Copyright (c) Zearain. All rights reserved.
// </copyright>

using System.Globalization;

using ErrorOr;

using MediatR;

using Zearain.AoC23.Application.AdventDays.Services.BoatRaces;
using Zearain.AoC23.Domain.AdventDayAggregate.Entities;

namespace Zearain.AoC23.Application.AdventDays.Solutions;

/// <summary>
/// Represents a handler for <see cref="SolveDay6Part1Command"/>.
/// </summary>
public class SolveDay6Part1CommandHandler : IRequestHandler<SolveDay6Part1Command, ErrorOr<PartSolution>>
{
    /// <inheritdoc />
    public Task<ErrorOr<PartSolution>> Handle(SolveDay6Part1Command request, CancellationToken cancellationToken)
    {
        var input = request.Input;

        var boatRaces = BoatRaceService.ReadBoatRaces(input);

        var waysToWin = new List<int>();
        foreach (var boatRace in boatRaces)
        {
            var winningInitialVelocities = BoatRaceService.GetWaysToWinRaceWithVelocity(boatRace);
            waysToWin.Add(winningInitialVelocities.Length);
        }

        var product = waysToWin.Aggregate(1, (total, next) => total * next);

        var solution = PartSolution.Create(request.PartNumber, product.ToString(CultureInfo.InvariantCulture));
        return Task.FromResult(solution);
    }
}