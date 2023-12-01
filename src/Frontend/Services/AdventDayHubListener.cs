// <copyright file="AdventDayHubListener.cs" company="Zearain">
// Copyright (c) Zearain. All rights reserved.
// </copyright>

using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.AspNetCore.SignalR.Client;

using Zearain.AoC23.Contracts.Events;
using Zearain.AoC23.Contracts.Responses;

namespace Zearain.AoC23.Frontend.Services;

/// <summary>
/// Represents a listener for the Advent Day Hub.
/// </summary>
public class AdventDayHubListener : IAdventDayHub
{
    private readonly ILogger logger;
    private readonly HubConnection hubConnection;

    /// <summary>
    /// Initializes a new instance of the <see cref="AdventDayHubListener"/> class.
    /// </summary>
    /// <param name="logger">The logger to use.</param>
    /// <param name="configuration">The configuration to use.</param>
    /// <param name="environment">The environment to use.</param>
    public AdventDayHubListener(ILogger<AdventDayHubListener> logger, IConfiguration configuration, IWebAssemblyHostEnvironment environment)
    {
        this.logger = logger;

        var apiUrl = (configuration.GetValue<string>("APIUrl") ?? environment.BaseAddress).TrimEnd('/');

        this.logger.LogInformation("Connecting to {ApiUrl}/adventdayhub", apiUrl);
        this.hubConnection = new HubConnectionBuilder()
            .WithUrl(apiUrl + "/adventdayhub")
            .WithAutomaticReconnect()
            .Build();

        this.hubConnection.On<AdventDayResponse>(nameof(this.AdventDayCreated), this.AdventDayCreated);
        this.hubConnection.On<Guid, string, bool>(nameof(this.DayInputAdded), this.DayInputAdded);
        this.hubConnection.On<Guid, PartSolutionResponse>(nameof(this.PartSolutionAdded), this.PartSolutionAdded);

        _ = this.StartConnectionAsync();
    }

    /// <summary>
    /// Gets or sets the event handler for when an Advent Day is created.
    /// </summary>
    public event Func<AdventDayResponse, Task>? OnAdventDayCreated;

    /// <summary>
    /// Gets or sets the event handler for when a Day Input is added.
    /// </summary>
    public event Func<Guid, string, bool, Task>? OnDayInputAdded;

    /// <summary>
    /// Gets or sets the event handler for when a Part Solution is added.
    /// </summary>
    public event Func<Guid, PartSolutionResponse, Task>? OnPartSolutionAdded;

    public async Task AdventDayCreated(AdventDayResponse adventDay)
    {
        this.logger.LogInformation("Advent Day created: {AdventDay}", adventDay);

        if (this.OnAdventDayCreated is not null)
        {
            await this.OnAdventDayCreated(adventDay);
        }
    }

    public async Task DayInputAdded(Guid adventDayId, string addedInput, bool hasInput)
    {
        this.logger.LogInformation("Day input added: {AdventDayId}, {AddedInput}, {HasInput}", adventDayId, addedInput, hasInput);

        if (this.OnDayInputAdded is not null)
        {
            await this.OnDayInputAdded(adventDayId, addedInput, hasInput);
        }
    }

    public async Task PartSolutionAdded(Guid adventDayId, PartSolutionResponse partSolution)
    {
        this.logger.LogInformation("Part solution added: {AdventDayId}, {PartSolution}", adventDayId, partSolution);

        if (this.OnPartSolutionAdded is not null)
        {
            await this.OnPartSolutionAdded(adventDayId, partSolution);
        }
    }

    private async Task StartConnectionAsync()
    {
        try
        {
            await this.hubConnection.StartAsync();
            this.logger.LogInformation("Connection started");
        }
        catch (Exception ex)
        {
            this.logger.LogError(ex, "Error starting connection");
        }
    }
}