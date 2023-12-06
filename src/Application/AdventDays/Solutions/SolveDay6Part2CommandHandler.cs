// <copyright file="SolveDay6Part2CommandHandler.cs" company="Zearain">
// Copyright (c) Zearain. All rights reserved.
// </copyright>

using System.Globalization;

using ErrorOr;

using MediatR;

using Zearain.AoC23.Application.AdventDays.Services.BoatRaces;
using Zearain.AoC23.Domain.AdventDayAggregate.Entities;

namespace Zearain.AoC23.Application.AdventDays.Solutions;

/// <summary>
/// Represents a handler for <see cref="SolveDay6Part2Command"/>.
/// </summary>
public class SolveDay6Part2CommandHandler : IRequestHandler<SolveDay6Part2Command, ErrorOr<PartSolution>>
{
    /// <inheritdoc />
    public Task<ErrorOr<PartSolution>> Handle(SolveDay6Part2Command request, CancellationToken cancellationToken)
    {
        var input = request.Input;

        var boatRace = BoatRaceService.ReadBoatRace(input);
        var waysToWin = BoatRaceService.GetWaysToWinRaceWithVelocity(boatRace);

        var solution = PartSolution.Create(request.PartNumber, waysToWin.Length.ToString(CultureInfo.InvariantCulture));
        return Task.FromResult(solution);
    }
}