// <copyright file="SetAdventDayInputCommandHandler.cs" company="Zearain">
// Copyright (c) Zearain. All rights reserved.
// </copyright>

using ErrorOr;

using MediatR;

using Zearain.AoC23.Application.Abstractions;
using Zearain.AoC23.Application.Repositories;

namespace Zearain.AoC23.Application.AdventDays.Commands;

/// <summary>
/// Represents a command handler for <see cref="SetAdventDayInputCommand"/>.
/// </summary>
public class SetAdventDayInputCommandHandler : IRequestHandler<SetAdventDayInputCommand, ErrorOr<Updated>>
{
    private readonly IAdventDayRepository adventDayRepository;
    private readonly IUnitOfWork unitOfWork;

    /// <summary>
    /// Initializes a new instance of the <see cref="SetAdventDayInputCommandHandler"/> class.
    /// </summary>
    /// <param name="adventDayRepository">The <see cref="IAdventDayRepository"/> to use.</param>
    public SetAdventDayInputCommandHandler(IAdventDayRepository adventDayRepository)
    {
        this.adventDayRepository = adventDayRepository;
        this.unitOfWork = this.adventDayRepository.UnitOfWork;
    }

    /// <inheritdoc/>
    public async Task<ErrorOr<Updated>> Handle(SetAdventDayInputCommand request, CancellationToken cancellationToken)
    {
        var adventDayResult = await this.adventDayRepository.GetByIdAsync(request.AdventDayId, cancellationToken);

        if (adventDayResult.IsError)
        {
            return adventDayResult.Errors;
        }

        var adventDay = adventDayResult.Value;
        adventDay.SetInput(request.Input);

        await this.adventDayRepository.UpdateAsync(adventDay, cancellationToken);
        await this.unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Updated;
    }
}