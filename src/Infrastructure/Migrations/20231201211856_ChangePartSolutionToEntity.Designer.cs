﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Zearain.AoC23.Infrastructure.Persistence;

#nullable disable

namespace Zearain.AoC23.Infrastructure.Migrations
{
    [DbContext(typeof(AoC23DbContext))]
    [Migration("20231201211856_ChangePartSolutionToEntity")]
    partial class ChangePartSolutionToEntity
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Zearain.AoC23.Domain.AdventDayAggregate.AdventDay", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<int>("DayNumber")
                        .HasColumnType("integer");

                    b.Property<bool>("HasInput")
                        .HasColumnType("boolean");

                    b.HasKey("Id");

                    b.ToTable("AdventDays", (string)null);
                });

            modelBuilder.Entity("Zearain.AoC23.Domain.AdventDayAggregate.AdventDay", b =>
                {
                    b.OwnsMany("Zearain.AoC23.Domain.AdventDayAggregate.Entities.PartSolution", "PartSolutions", b1 =>
                        {
                            b1.Property<Guid>("Id")
                                .HasColumnType("uuid");

                            b1.Property<Guid>("AdventDayId")
                                .HasColumnType("uuid");

                            b1.Property<int>("PartNumber")
                                .HasColumnType("integer");

                            b1.Property<string>("Solution")
                                .HasColumnType("text");

                            b1.HasKey("Id", "AdventDayId");

                            b1.HasIndex("AdventDayId");

                            b1.ToTable("PartSolutions", (string)null);

                            b1.WithOwner()
                                .HasForeignKey("AdventDayId");
                        });

                    b.OwnsOne("Zearain.AoC23.Domain.AdventDayAggregate.ValueObjects.DayInput", "Input", b1 =>
                        {
                            b1.Property<Guid>("AdventDayId")
                                .HasColumnType("uuid");

                            b1.Property<string>("RawInput")
                                .HasColumnType("text")
                                .HasColumnName("Input");

                            b1.HasKey("AdventDayId");

                            b1.ToTable("AdventDays");

                            b1.WithOwner()
                                .HasForeignKey("AdventDayId");
                        });

                    b.Navigation("Input");

                    b.Navigation("PartSolutions");
                });
#pragma warning restore 612, 618
        }
    }
}
