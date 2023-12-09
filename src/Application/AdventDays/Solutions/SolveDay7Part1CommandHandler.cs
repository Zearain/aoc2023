﻿// <copyright file="SolveDay7Part1CommandHandler.cs" company="Zearain">
// Copyright (c) Zearain. All rights reserved.
// </copyright>

using System.Globalization;

using ErrorOr;

using MediatR;

using Zearain.AoC23.Application.AdventDays.Services.CamelCards;
using Zearain.AoC23.Domain.AdventDayAggregate.Entities;

namespace Zearain.AoC23.Application.AdventDays.Solutions;

/// <summary>
/// Represents a handler for <see cref="SolveDay7Part1Command"/>.
/// </summary>
public class SolveDay7Part1CommandHandler : IRequestHandler<SolveDay7Part1Command, ErrorOr<PartSolution>>
{
    /// <inheritdoc />
    public Task<ErrorOr<PartSolution>> Handle(SolveDay7Part1Command request, CancellationToken cancellationToken)
    {
        var input = request.Input;

        var product = CamelCardGameService.GetTotalWinnings(CamelCardGameService.ReadHands(input));

        var solution = PartSolution.Create(request.PartNumber, product.ToString(CultureInfo.InvariantCulture));
        return Task.FromResult(solution);
    }
}