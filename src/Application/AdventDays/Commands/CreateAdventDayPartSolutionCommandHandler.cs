// <copyright file="CreateAdventDayPartSolutionCommandHandler.cs" company="Zearain">
// Copyright (c) Zearain. All rights reserved.
// </copyright>

using System.Reflection;

using ErrorOr;

using MediatR;

using Zearain.AoC23.Application.Abstractions;
using Zearain.AoC23.Application.Attributes;
using Zearain.AoC23.Application.Repositories;
using Zearain.AoC23.Domain.AdventDayAggregate;
using Zearain.AoC23.Domain.AdventDayAggregate.Errors;

namespace Zearain.AoC23.Application;

/// <summary>
/// Represents a command handler for <see cref="CreateAdventDayPartSolutionCommand"/>.
/// </summary>
public class CreateAdventDayPartSolutionCommandHandler : IRequestHandler<CreateAdventDayPartSolutionCommand, ErrorOr<Updated>>
{
    private readonly IAdventDayRepository adventDayRepository;
    private readonly IUnitOfWork unitOfWork;
    private readonly ISender sender;

    /// <summary>
    /// Initializes a new instance of the <see cref="CreateAdventDayPartSolutionCommandHandler"/> class.
    /// </summary>
    /// <param name="adventDayRepository">The <see cref="IAdventDayRepository"/> to use.</param>
    /// <param name="sender">The <see cref="ISender"/> to use.</param>
    public CreateAdventDayPartSolutionCommandHandler(IAdventDayRepository adventDayRepository, ISender sender)
    {
        this.adventDayRepository = adventDayRepository;
        this.unitOfWork = this.adventDayRepository.UnitOfWork;
        this.sender = sender;
    }

    /// <inheritdoc/>
    public async Task<ErrorOr<Updated>> Handle(CreateAdventDayPartSolutionCommand request, CancellationToken cancellationToken)
    {
        var adventDayResult = await this.adventDayRepository.GetByIdAsync(request.AdventDayId, cancellationToken);

        if (adventDayResult.IsError)
        {
            return adventDayResult.Errors;
        }

        var adventDay = adventDayResult.Value;
        if (adventDay.HasInput == false)
        {
            return AdventDayErrors.NoInput;
        }

        var adventDayPartSolutionRequest = CreateAdventDayPartSolutionRequest(adventDay, request.PartNumber);
        if (adventDayPartSolutionRequest is null)
        {
            return Error.Failure("CreateAdventDayPartSolutionCommandHandler.RequestNotImplemented", $"The request to solve day {adventDay.DayNumber} part {request.PartNumber} is not implemented.");
        }

        var adventDayPartSolutionResult = await this.sender.Send(adventDayPartSolutionRequest, cancellationToken);
        if (adventDayPartSolutionResult.IsError)
        {
            return adventDayPartSolutionResult.Errors;
        }

        var addResult = adventDay.AddPartSolution(adventDayPartSolutionResult.Value);
        if (addResult.IsError)
        {
            return addResult.Errors;
        }

        var updateResult = await this.adventDayRepository.UpdateAsync(adventDay, cancellationToken);
        if (updateResult.IsError)
        {
            return updateResult.Errors;
        }

        await this.unitOfWork.SaveChangesAsync(cancellationToken);
        return Result.Updated;
    }

    private static IAdventDayPartSolutionRequest? CreateAdventDayPartSolutionRequest(AdventDay adventDay, int partNumber)
    {
        var requestType = GetImplementedDaySolverTypes()
            .FirstOrDefault(x => x.GetCustomAttribute<AdventDayPartAttribute>()?.DayNumber == adventDay.DayNumber &&
                x.GetCustomAttribute<AdventDayPartAttribute>()?.PartNumber == partNumber);

        return requestType is null
            ? null
            : Activator.CreateInstance(requestType, new object[] { partNumber, adventDay.Input! }) as IAdventDayPartSolutionRequest;
    }

    private static IEnumerable<Type> GetImplementedDaySolverTypes()
    {
        return typeof(IAdventDayPartSolutionRequest).Assembly.GetTypes()
            .Where(x => typeof(IAdventDayPartSolutionRequest).IsAssignableFrom(x) &&
                !x.IsInterface &&
                !x.IsAbstract &&
                x.GetCustomAttribute<AdventDayPartAttribute>() is not null);
    }
}