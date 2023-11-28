namespace Zearain.AoC23.Domain.AdventDays;

using Zearain.AoC23.Domain.AdventDays.Events;
using Zearain.AoC23.Domain.Common;

public class AdventDay : AggregateRoot<DayId, Guid>
{
    public bool HasInput { get; private set; }

    public DayInput? Input { get; private set; }

    public void SetInput(DayInput input)
    {
        this.HasInput = true;
        this.Input = input;
        this.AddDomainEvent(new DayInputAdded(this.Id, input));
    }
}
