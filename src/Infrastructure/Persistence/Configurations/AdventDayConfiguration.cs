// <copyright file="AdventDayConfiguration.cs" company="Zearain">
// Copyright (c) Zearain. All rights reserved.
// </copyright>

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Zearain.AoC23.Domain.AdventDayAggregate;
using Zearain.AoC23.Domain.AdventDayAggregate.Entities;
using Zearain.AoC23.Domain.AdventDayAggregate.ValueObjects;

namespace Zearain.AoC23.Infrastructure.Persistence.Configurations;

/// <summary>
/// Contains the configuration for the <see cref="AdventDay"/> entity.
/// </summary>
public class AdventDayConfiguration : IEntityTypeConfiguration<AdventDay>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<AdventDay> builder)
    {
        ConfigureAdventDaysTables(builder);
        ConfigurePartSolutionsTables(builder);
    }

    private static void ConfigureAdventDaysTables(EntityTypeBuilder<AdventDay> builder)
    {
        builder.ToTable("AdventDays");

        builder.HasKey(ad => ad.Id);

        builder.Property(ad => ad.Id)
            .ValueGeneratedNever()
            .HasConversion(
                id => id.Value,
                value => AdventDayId.Create(value))
            .IsRequired();

        builder.Property(ad => ad.DayNumber)
            .IsRequired();

        builder.Property(ad => ad.HasInput)
            .IsRequired();

        builder.OwnsOne(
            ad => ad.Input,
            input =>
            {
                input.Property(i => i.RawInput)
                    .HasColumnName("Input")
                    .IsRequired(false);
            });
    }

    private static void ConfigurePartSolutionsTables(EntityTypeBuilder<AdventDay> builder)
    {
        builder.OwnsMany(
            ad => ad.PartSolutions,
            psb =>
            {
                psb.ToTable("PartSolutions");

                psb.WithOwner().HasForeignKey("AdventDayId");

                psb.HasKey(nameof(PartSolution.Id), "AdventDayId");

                psb.Property(ps => ps.Id)
                    .ValueGeneratedNever()
                    .HasConversion(
                        id => id.Value,
                        value => PartSolutionId.Create(value))
                    .IsRequired();

                psb.Property(ps => ps.PartNumber)
                    .IsRequired();

                psb.Property(ps => ps.Solution)
                    .IsRequired(false);
            });

        builder.Metadata.FindNavigation(nameof(AdventDay.PartSolutions))!.SetPropertyAccessMode(PropertyAccessMode.Field);
    }
}