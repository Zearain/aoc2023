// <copyright file="GetAllAdventDaysQueryHandler.cs" company="Zearain">
// Copyright (c) Zearain. All rights reserved.
// </copyright>

using ErrorOr;

using MediatR;

using Zearain.AoC23.Application.Repositories;
using Zearain.AoC23.Domain.AdventDayAggregate;

namespace Zearain.AoC23.Application.AdventDays.Queries;

/// <summary>
/// Represents a query handler for <see cref="GetAllAdventDaysQuery"/>.
/// </summary>
public class GetAllAdventDaysQueryHandler : IRequestHandler<GetAllAdventDaysQuery, ErrorOr<IEnumerable<AdventDay>>>
{
    private readonly IAdventDayRepository adventDayRepository;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetAllAdventDaysQueryHandler"/> class.
    /// </summary>
    /// <param name="adventDayRepository">The <see cref="IAdventDayRepository"/> to use.</param>
    public GetAllAdventDaysQueryHandler(IAdventDayRepository adventDayRepository)
    {
        this.adventDayRepository = adventDayRepository;
    }

    /// <inheritdoc/>
    public async Task<ErrorOr<IEnumerable<AdventDay>>> Handle(GetAllAdventDaysQuery request, CancellationToken cancellationToken)
    {
        var adventDays = new List<AdventDay>();
        await foreach (var adventDay in this.adventDayRepository.GetAllAsync(cancellationToken))
        {
            adventDays.Add(adventDay);
        }

        return adventDays;
    }
}