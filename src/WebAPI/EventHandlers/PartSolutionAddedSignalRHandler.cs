// <copyright file="PartSolutionAddedSignalRHandler.cs" company="Zearain">
// Copyright (c) Zearain. All rights reserved.
// </copyright>

using MediatR;

using Microsoft.AspNetCore.SignalR;

using Zearain.AoC23.Contracts.Events;
using Zearain.AoC23.Contracts.Responses;
using Zearain.AoC23.Domain;
using Zearain.AoC23.WebAPI.Hubs;

namespace Zearain.AoC23.WebAPI;

/// <summary>
/// Represents a SignalR handler for <see cref="PartSolutionAdded"/>.
/// </summary>
public class PartSolutionAddedSignalRHandler : INotificationHandler<PartSolutionAdded>
{
    private readonly IHubContext<AdventDayHub, IAdventDayHub> hubContext;

    /// <summary>
    /// Initializes a new instance of the <see cref="PartSolutionAddedSignalRHandler"/> class.
    /// </summary>
    /// <param name="hubContext">The hub context to use.</param>
    public PartSolutionAddedSignalRHandler(IHubContext<AdventDayHub, IAdventDayHub> hubContext)
    {
        this.hubContext = hubContext;
    }

    /// <inheritdoc/>
    public async Task Handle(PartSolutionAdded notification, CancellationToken cancellationToken)
    {
        await this.hubContext.Clients.All.PartSolutionAdded(notification.AdventDayId.Value, new PartSolutionResponse
        {
            PartNumber = notification.PartSolution.PartNumber,
            Solution = notification.PartSolution.Solution,
        });
    }
}