namespace Zearain.AoC23.Domain.AdventDays.Events;

using Zearain.AoC23.Domain.AdventDays;
using Zearain.AoC23.Domain.Common;

public record DayInputAdded(DayId DayId, DayInput AddedInput) : IDomainEvent;
