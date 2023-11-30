// <copyright file="GetAdventDayQueryHandler.cs" company="Zearain">
// Copyright (c) Zearain. All rights reserved.
// </copyright>

using ErrorOr;

using MediatR;

using Zearain.AoC23.Application.Repositories;
using Zearain.AoC23.Domain.AdventDayAggregate;

namespace Zearain.AoC23.Application.AdventDays.Queries;

/// <summary>
/// Represents a query handler to get an Advent Day.
/// </summary>
public class GetAdventDayQueryHandler : IRequestHandler<GetAdventDayQuery, ErrorOr<AdventDay>>
{
    private readonly IAdventDayRepository adventDayRepository;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetAdventDayQueryHandler"/> class.
    /// </summary>
    /// <param name="adventDayRepository">The repository to use.</param>
    public GetAdventDayQueryHandler(IAdventDayRepository adventDayRepository)
    {
        this.adventDayRepository = adventDayRepository;
    }

    /// <inheritdoc/>
    public async Task<ErrorOr<AdventDay>> Handle(GetAdventDayQuery request, CancellationToken cancellationToken)
    {
        return await this.adventDayRepository.GetByIdAsync(request.Id, cancellationToken);
    }
}
