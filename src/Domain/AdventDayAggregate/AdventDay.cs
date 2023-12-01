// <copyright file="AdventDay.cs" company="Zearain">
// Copyright (c) Zearain. All rights reserved.
// </copyright>

using ErrorOr;

using Zearain.AoC23.Domain.AdventDayAggregate.Entities;
using Zearain.AoC23.Domain.AdventDayAggregate.Errors;
using Zearain.AoC23.Domain.AdventDayAggregate.Events;
using Zearain.AoC23.Domain.AdventDayAggregate.ValueObjects;
using Zearain.AoC23.Domain.Common;

namespace Zearain.AoC23.Domain.AdventDayAggregate;

/// <summary>
/// Represents an advent day.
/// </summary>
public class AdventDay : AggregateRoot<AdventDayId, Guid>
{
    private readonly List<PartSolution> partSolutions = new();

    private AdventDay()
    {
    }

    private AdventDay(AdventDayId id, int dayNumber)
    {
        this.Id = id;
        this.DayNumber = dayNumber;
    }

    /// <summary>
    /// Gets the day number.
    /// </summary>
    public int DayNumber { get; private set; }

    /// <summary>
    /// Gets a value indicating whether the <see cref="AdventDay"/> has an input.
    /// </summary>
    public bool HasInput { get; private set; }

    /// <summary>
    /// Gets the input of the <see cref="AdventDay"/>.
    /// </summary>
    public DayInput? Input { get; private set; }

    /// <summary>
    /// Gets the solutions to the parts of the <see cref="AdventDay"/>.
    /// </summary>
    public IReadOnlyCollection<PartSolution> PartSolutions => this.partSolutions.AsReadOnly();

    /// <summary>
    /// Creates a new <see cref="AdventDay"/> for the given day number.
    /// </summary>
    /// <param name="dayNumber">The day number.</param>
    /// <returns>A new instance of <see cref="AdventDay"/>.</returns>
    public static ErrorOr<AdventDay> Create(int dayNumber)
    {
        if (dayNumber < 1 || dayNumber > 25)
        {
            return AdventDayErrors.InvalidDayNumber;
        }

        var adventDay = new AdventDay(AdventDayId.New(), dayNumber);
        adventDay.AddDomainEvent(new AdventDayCreated(adventDay));

        return adventDay;
    }

    /// <summary>
    /// Sets the input of the <see cref="AdventDay"/>.
    /// </summary>
    /// <param name="input">The input to set.</param>
    public void SetInput(string input)
    {
        if (this.Input is null)
        {
            this.Input = DayInput.Create(input);
        }
        else
        {
            this.Input.Update(input);
        }

        this.HasInput = !string.IsNullOrEmpty(this.Input.RawInput);

        this.AddDomainEvent(new DayInputAdded((AdventDayId)this.Id, this.Input, this.HasInput));
    }

    /// <summary>
    /// Adds a solution to the <see cref="AdventDay"/>.
    /// </summary>
    /// <param name="partSolution">The solution to add.</param>
    /// <returns>A value indicating whether the <see cref="AdventDay"/> was updated.</returns>
    public ErrorOr<Updated> AddPartSolution(PartSolution partSolution)
    {
        if (this.HasInput == false)
        {
            return AdventDayErrors.NoInput;
        }

        if (this.partSolutions.FirstOrDefault(ps => ps.PartNumber == partSolution.PartNumber) is PartSolution existingPartSolution)
        {
            existingPartSolution.Update(partSolution.Solution);
            this.AddDomainEvent(new PartSolutionAdded((AdventDayId)this.Id, partSolution));
            return Result.Updated;
        }

        if (this.partSolutions.Count != 0 &&
            this.partSolutions.Max(ps => ps.PartNumber) != partSolution.PartNumber - 1)
        {
            return AdventDayErrors.InvalidSolutionOrder;
        }

        this.partSolutions.Add(partSolution);
        this.AddDomainEvent(new PartSolutionAdded((AdventDayId)this.Id, partSolution));
        return Result.Updated;
    }
}