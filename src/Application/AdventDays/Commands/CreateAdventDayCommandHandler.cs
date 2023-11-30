// <copyright file="CreateAdventDayCommandHandler.cs" company="Zearain">
// Copyright (c) Zearain. All rights reserved.
// </copyright>

using ErrorOr;

using MediatR;

using Zearain.AoC23.Application.Abstractions;
using Zearain.AoC23.Application.Repositories;
using Zearain.AoC23.Domain.AdventDayAggregate;
using Zearain.AoC23.Domain.AdventDayAggregate.Errors;

namespace Zearain.AoC23.Application.AdventDays.Commands;

/// <summary>
/// Represents a command handler for <see cref="CreateAdventDayCommand"/>.
/// </summary>
public class CreateAdventDayCommandHandler : IRequestHandler<CreateAdventDayCommand, ErrorOr<AdventDay>>
{
    private readonly IAdventDayRepository adventDayRepository;
    private readonly IUnitOfWork unitOfWork;

    /// <summary>
    /// Initializes a new instance of the <see cref="CreateAdventDayCommandHandler"/> class.
    /// </summary>
    /// <param name="adventDayRepository">The <see cref="IAdventDayRepository"/> to use.</param>
    public CreateAdventDayCommandHandler(IAdventDayRepository adventDayRepository)
    {
        this.adventDayRepository = adventDayRepository;
        this.unitOfWork = this.adventDayRepository.UnitOfWork;
    }

    /// <inheritdoc/>
    public async Task<ErrorOr<AdventDay>> Handle(CreateAdventDayCommand request, CancellationToken cancellationToken)
    {
        if ((await this.adventDayRepository.GetByDayNumberAsync(request.DayNumber)).IsError == false)
        {
            // This means we already have an AdventDay with the specified day number.
            return AdventDayErrors.DuplicateDayNumber;
        }

        var adventDay = AdventDay.Create(request.DayNumber);
        if (adventDay.IsError)
        {
            return adventDay.Errors;
        }

        var addedAdventDay = await this.adventDayRepository.AddAsync(adventDay.Value, cancellationToken);
        await this.unitOfWork.SaveChangesAsync(cancellationToken);

        return addedAdventDay;
    }
}