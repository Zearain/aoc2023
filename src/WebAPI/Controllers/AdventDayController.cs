// <copyright file="AdventDayController.cs" company="Zearain">
// Copyright (c) Zearain. All rights reserved.
// </copyright>

using MediatR;

using Microsoft.AspNetCore.Mvc;

using Zearain.AoC23.Application;
using Zearain.AoC23.Application.AdventDays.Commands;
using Zearain.AoC23.Application.AdventDays.Queries;
using Zearain.AoC23.Contracts.Responses;
using Zearain.AoC23.Domain.AdventDayAggregate.ValueObjects;

namespace Zearain.AoC23.WebAPI;

/// <summary>
/// Represents the controller for Advent Days.
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class AdventDayController : ControllerBase
{
    private readonly ILogger logger;
    private readonly ISender sender;

    /// <summary>
    /// Initializes a new instance of the <see cref="AdventDayController"/> class.
    /// </summary>
    /// <param name="logger">The logger to use.</param>
    /// <param name="sender">The sender to use.</param>
    public AdventDayController(ILogger<AdventDayController> logger, ISender sender)
    {
        this.logger = logger;
        this.sender = sender;
    }

    /// <summary>
    /// Gets all Advent Days.
    /// </summary>
    /// <returns>All Advent Days.</returns>
    [HttpGet]
    public async Task<IActionResult> GetAdventDaysAsync()
    {
        var result = await this.sender.Send(new GetAllAdventDaysQuery());

        if (result.IsError)
        {
            return this.Problem($"{result.Errors.Count} errors occurred while getting Advent Days: {string.Join(", ", result.Errors)}");
        }

        var adventDayResponses = result.Value.Select(adventDay => new AdventDayResponse
        {
            Id = adventDay.Id.Value,
            DayNumber = adventDay.DayNumber,
            HasInput = adventDay.HasInput,
            Input = adventDay.Input?.RawInput,
            PartSolutions = adventDay.PartSolutions.Select(partSolution => new PartSolutionResponse
            {
                PartNumber = partSolution.PartNumber,
                Solution = partSolution.Solution,
            }),
        });

        return this.Ok(adventDayResponses);
    }

    /// <summary>
    /// Gets an Advent Day by ID.
    /// </summary>
    /// <param name="id">The ID of the Advent Day.</param>
    /// <returns>The Advent Day.</returns>
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetAdventDayAsync(Guid id)
    {
        var result = await this.sender.Send(new GetAdventDayQuery(AdventDayId.Create(id)));

        return result.Match(
            adventDay => this.Ok(new AdventDayResponse
            {
                Id = adventDay.Id.Value,
                DayNumber = adventDay.DayNumber,
                HasInput = adventDay.HasInput,
                Input = adventDay.Input?.RawInput,
                PartSolutions = adventDay.PartSolutions.Select(partSolution => new PartSolutionResponse
                {
                    PartNumber = partSolution.PartNumber,
                    Solution = partSolution.Solution,
                }),
            }),
            errors => this.Problem($"{errors.Count} errors occurred while getting Advent Day: {string.Join(", ", errors)}"));
    }

    /// <summary>
    /// Creates a new advent day with the given day number.
    /// </summary>
    /// <param name="dayNumber">The day number of the new advent day.</param>
    /// <returns>The new advent day.</returns>
    [HttpPost]
    public async Task<IActionResult> CreateAdventDayAsync(int dayNumber)
    {
        var result = await this.sender.Send(new CreateAdventDayCommand(dayNumber));

        return result.Match(
            result => this.CreatedAtAction(nameof(this.GetAdventDayAsync), new { id = result.Id.Value }, new AdventDayResponse
            {
                Id = result.Id.Value,
                DayNumber = result.DayNumber,
                HasInput = result.HasInput,
                Input = result.Input?.RawInput,
                PartSolutions = result.PartSolutions.Select(partSolution => new PartSolutionResponse
                {
                    PartNumber = partSolution.PartNumber,
                    Solution = partSolution.Solution,
                }),
            }),
            errors => this.Problem($"{errors.Count} errors occurred while creating Advent Day: {string.Join(", ", errors)}"));
    }

    /// <summary>
    /// Solves a part of an Advent Day.
    /// </summary>
    /// <param name="id">The ID of the Advent Day.</param>
    /// <param name="partNumber">The part number to solve.</param>
    /// <returns>A result indicating whether the part was solved.</returns>
    [HttpGet("{id:guid}/solve/{partNumber:int}")]
    public async Task<IActionResult> SolvePartAsync(Guid id, int partNumber)
    {
        var result = await this.sender.Send(new CreateAdventDayPartSolutionCommand(AdventDayId.Create(id), partNumber));

        return result.Match(
            result => this.Ok(result),
            errors => this.Problem($"{errors.Count} errors occurred while solving part: {string.Join(", ", errors)}"));
    }
}