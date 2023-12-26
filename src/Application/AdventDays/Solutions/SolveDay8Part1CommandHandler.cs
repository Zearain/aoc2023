// <copyright file="SolveDay8Part1CommandHandler.cs" company="Zearain">
// Copyright (c) Zearain. All rights reserved.
// </copyright>

using System.Globalization;

using ErrorOr;

using MediatR;

using Zearain.AoC23.Application.AdventDays.Services.CamelNetworkMaps;
using Zearain.AoC23.Domain.AdventDayAggregate.Entities;

namespace Zearain.AoC23.Application.AdventDays.Solutions;

/// <summary>
/// Represents a handler for <see cref="SolveDay8Part1Command"/>.
/// </summary>
public class SolveDay8Part1CommandHandler : IRequestHandler<SolveDay8Part1Command, ErrorOr<PartSolution>>
{
    /// <inheritdoc />
    public Task<ErrorOr<PartSolution>> Handle(SolveDay8Part1Command request, CancellationToken cancellationToken)
    {
        var input = request.Input;

        var map = CamelNetworkMap.Read(input);

        var instructionCosts = map.GetCostOfInstructions();

        var solution = PartSolution.Create(request.PartNumber, instructionCosts.ToString(CultureInfo.InvariantCulture));
        return Task.FromResult(solution);
    }
}