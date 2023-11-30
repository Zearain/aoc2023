// <copyright file="SetAdventDayInputCommand.cs" company="Zearain">
// Copyright (c) Zearain. All rights reserved.
// </copyright>

using ErrorOr;

using MediatR;

using Zearain.AoC23.Domain.AdventDayAggregate;
using Zearain.AoC23.Domain.AdventDayAggregate.ValueObjects;

namespace Zearain.AoC23.Application.AdventDays.Commands;

/// <summary>
/// Represents a command to set the input of an <see cref="AdventDay"/>.
/// </summary>
/// <param name="AdventDayId">The ID of the <see cref="AdventDay"/>.</param>
/// <param name="Input">The input to set.</param>
public record SetAdventDayInputCommand(AdventDayId AdventDayId, string Input) : IRequest<ErrorOr<Updated>>;