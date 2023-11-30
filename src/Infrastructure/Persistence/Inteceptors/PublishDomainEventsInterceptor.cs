// <copyright file="PublishDomainEventsInterceptor.cs" company="Zearain">
// Copyright (c) Zearain. All rights reserved.
// </copyright>

using MediatR;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

using Zearain.AoC23.Domain.Common;

namespace Zearain.AoC23.Infrastructure.Persistence.Inteceptors;

/// <summary>
/// Represents an interceptor for publishing domain events when saving changes.
/// </summary>
public class PublishDomainEventsInterceptor : SaveChangesInterceptor
{
    private readonly IPublisher publisher;

    /// <summary>
    /// Initializes a new instance of the <see cref="PublishDomainEventsInterceptor"/> class.
    /// </summary>
    /// <param name="publisher">The publisher to use.</param>
    public PublishDomainEventsInterceptor(IPublisher publisher)
    {
        this.publisher = publisher;
    }

    /// <inheritdoc/>
    public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
    {
        throw new NotSupportedException("Publishing domain events is not supported synchronously.");
    }

    /// <inheritdoc/>
    public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
    {
        await this.PublishDomainEventsAsync(eventData.Context);
        return await base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    private async Task PublishDomainEventsAsync(DbContext? context)
    {
        if (context is null)
        {
            return;
        }

        var domainEventEntities = context.ChangeTracker.Entries<IDomainEventContainer>()
            .Where(entry => entry.Entity.DomainEvents.Any())
            .Select(entry => entry.Entity)
            .ToList();

        var domainEvents = domainEventEntities
            .SelectMany(entity => entity.DomainEvents)
            .ToArray();

        domainEventEntities.ForEach(entity => entity.ClearDomainEvents());

        foreach (var domainEvent in domainEvents)
        {
            await this.publisher.Publish(domainEvent);
        }
    }
}