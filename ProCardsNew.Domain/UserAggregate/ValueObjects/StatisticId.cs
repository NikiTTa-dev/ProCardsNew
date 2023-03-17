using ProCardsNew.Domain.Common.Models;

namespace ProCardsNew.Domain.UserAggregate.ValueObjects;

public sealed class StatisticId: ValueObject
{
    public Guid Value { get; }

    private StatisticId(Guid value)
    {
        Value = value;
    }

    public static StatisticId CreateUnique()
    {
        return new(Guid.NewGuid());
    }
    
    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}