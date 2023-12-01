// <copyright file="AdventDayHub.cs" company="Zearain">
// Copyright (c) Zearain. All rights reserved.
// </copyright>

using Microsoft.AspNetCore.SignalR;

using Zearain.AoC23.Contracts.Events;

namespace Zearain.AoC23.WebAPI.Hubs;

/// <summary>
/// Represents the Advent Day SignalR Hub.
/// </summary>
public class AdventDayHub : Hub<IAdventDayHub>
{
}