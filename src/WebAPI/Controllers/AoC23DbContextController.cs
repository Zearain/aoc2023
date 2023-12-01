// <copyright file="AoC23DbContextController.cs" company="Zearain">
// Copyright (c) Zearain. All rights reserved.
// </copyright>

using Microsoft.AspNetCore.Mvc;

using Zearain.AoC23.Infrastructure.Persistence;

namespace Zearain.AoC23.WebAPI;

/// <summary>
/// Represents a controller for the AoC23DbContext.
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class AoC23DbContextController : ControllerBase
{
    private readonly AoC23DbContext context;

    /// <summary>
    /// Initializes a new instance of the <see cref="AoC23DbContextController"/> class.
    /// </summary>
    /// <param name="context">The context to use.</param>
    public AoC23DbContextController(AoC23DbContext context)
    {
        this.context = context;
    }

    /// <summary>
    /// Gets the AoC23DbContext.
    /// </summary>
    /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
    [HttpGet("reset")]
    public async Task<IActionResult> Reset()
    {
        await this.context.Database.EnsureDeletedAsync();
        await this.context.Database.EnsureCreatedAsync();
        return this.Ok();
    }
}