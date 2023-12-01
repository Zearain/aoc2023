// <copyright file="AdventDayCreatedSignalRHandler.cs" company="Zearain">
// Copyright (c) Zearain. All rights reserved.
// </copyright>

using MediatR;

using Microsoft.AspNetCore.SignalR;

using Zearain.AoC23.Contracts.Events;
using Zearain.AoC23.Contracts.Responses;
using Zearain.AoC23.Domain.AdventDayAggregate.Events;
using Zearain.AoC23.WebAPI.Hubs;

namespace Zearain.AoC23.WebAPI;

/// <summary>
/// Represents a SignalR handler for <see cref="AdventDayCreated"/>.
/// </summary>
public class AdventDayCreatedSignalRHandler : INotificationHandler<AdventDayCreated>
{
    private readonly IHubContext<AdventDayHub, IAdventDayHub> hubContext;

    /// <summary>
    /// Initializes a new instance of the <see cref="AdventDayCreatedSignalRHandler"/> class.
    /// </summary>
    /// <param name="hubContext">The hub context to use.</param>
    public AdventDayCreatedSignalRHandler(IHubContext<AdventDayHub, IAdventDayHub> hubContext)
    {
        this.hubContext = hubContext;
    }

    /// <inheritdoc/>
    public async Task Handle(AdventDayCreated notification, CancellationToken cancellationToken)
    {
        await this.hubContext.Clients.All.AdventDayCreated(new AdventDayResponse
        {
            Id = notification.AdventDay.Id.Value,
            DayNumber = notification.AdventDay.DayNumber,
            HasInput = notification.AdventDay.HasInput,
            Input = notification.AdventDay.Input?.RawInput,
            PartSolutions = notification.AdventDay.PartSolutions.Select(partSolution => new PartSolutionResponse
            {
                PartNumber = partSolution.PartNumber,
                Solution = partSolution.Solution,
            }).ToList(),
        });
    }
}