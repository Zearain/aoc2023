namespace Zearain.AoC23.Domain.AdventDays;

using Zearain.AoC23.Domain.Common;

public sealed record DayId : AggregateRootId<Guid>
{
    private DayId()
    {
    }

    private DayId(Guid value)
    {
        Value = value;
    }

    public override Guid Value { get; protected set; }

    public static DayId New() => new(Guid.NewGuid());

    public static DayId Create(Guid value) => new(value);
}
