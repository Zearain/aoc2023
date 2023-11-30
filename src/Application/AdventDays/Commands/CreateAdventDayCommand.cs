// <copyright file="CreateAdventDayCommand.cs" company="Zearain">
// Copyright (c) Zearain. All rights reserved.
// </copyright>

using ErrorOr;

using MediatR;

using Zearain.AoC23.Domain.AdventDayAggregate;

namespace Zearain.AoC23.Application.AdventDays.Commands;

/// <summary>
/// Represents a command to create an <see cref="AdventDay"/>.
/// </summary>
/// <param name="DayNumber">The day number of the <see cref="AdventDay"/> to create.</param>
public record CreateAdventDayCommand(int DayNumber) : IRequest<ErrorOr<AdventDay>>;