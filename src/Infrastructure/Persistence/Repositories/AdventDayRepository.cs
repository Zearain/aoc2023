// <copyright file="AdventDayRepository.cs" company="Zearain">
// Copyright (c) Zearain. All rights reserved.
// </copyright>

using ErrorOr;
using Microsoft.EntityFrameworkCore;

using Zearain.AoC23.Application.Abstractions;
using Zearain.AoC23.Application.Repositories;
using Zearain.AoC23.Domain.AdventDayAggregate;
using Zearain.AoC23.Domain.AdventDayAggregate.ValueObjects;
using Zearain.AoC23.Domain.Common;

namespace Zearain.AoC23.Infrastructure.Persistence.Repositories;

/// <inheritdoc cref="IAdventDayRepository"/>
public sealed class AdventDayRepository : Repository<AdventDay, AdventDayId>, IAdventDayRepository
{
    /// <summary>
    /// Initializes a new instance of the <see cref="AdventDayRepository"/> class.
    /// </summary>
    /// <param name="dbContext">The <see cref="AoC23DbContext"/> to use.</param>
    public AdventDayRepository(AoC23DbContext dbContext)
        : base(dbContext)
    {
    }

    /// <summary>
    /// Gets the <see cref="IUnitOfWork"/> for the repository.
    /// </summary>
    public override IUnitOfWork UnitOfWork => (AoC23DbContext)this.dbContext;

    /// <inheritdoc/>
    public async Task<ErrorOr<AdventDay>> GetByDayNumberAsync(int dayNumber)
    {
        var adventDay = await this.dbSet
            .AsNoTracking()
            .SingleOrDefaultAsync(ad => ad.DayNumber == dayNumber);

        return adventDay is null
            ? Error.NotFound()
            : adventDay;
    }

    /// <inheritdoc/>
    public override async Task<ErrorOr<AdventDay>> GetByIdAsync(AdventDayId id, CancellationToken cancellationToken = default)
    {
        var adventDay = await this.dbSet.FirstOrDefaultAsync(ad => ad.Id == id, cancellationToken);
        return adventDay is null
            ? Error.NotFound()
            : adventDay;
    }
}