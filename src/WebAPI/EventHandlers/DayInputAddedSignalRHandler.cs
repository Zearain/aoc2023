// <copyright file="DayInputAddedSignalRHandler.cs" company="Zearain">
// Copyright (c) Zearain. All rights reserved.
// </copyright>

using MediatR;

using Microsoft.AspNetCore.SignalR;

using Zearain.AoC23.Contracts.Events;
using Zearain.AoC23.Domain.AdventDayAggregate.Events;
using Zearain.AoC23.WebAPI.Hubs;

namespace Zearain.AoC23.WebAPI;

/// <summary>
/// Represents a SignalR handler for <see cref="DayInputAdded"/>.
/// </summary>
public class DayInputAddedSignalRHandler : INotificationHandler<DayInputAdded>
{
    private readonly IHubContext<AdventDayHub, IAdventDayHub> hubContext;

    /// <summary>
    /// Initializes a new instance of the <see cref="DayInputAddedSignalRHandler"/> class.
    /// </summary>
    /// <param name="hubContext">The hub context to use.</param>
    public DayInputAddedSignalRHandler(IHubContext<AdventDayHub, IAdventDayHub> hubContext)
    {
        this.hubContext = hubContext;
    }

    /// <inheritdoc/>
    public async Task Handle(DayInputAdded notification, CancellationToken cancellationToken)
    {
        await this.hubContext.Clients.All.DayInputAdded(
            notification.AdventDayId.Value, notification.AddedInput?.RawInput ?? string.Empty, notification.HasInput);
    }
}