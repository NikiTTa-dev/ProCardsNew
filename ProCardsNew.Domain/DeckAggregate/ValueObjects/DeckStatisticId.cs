using ProCardsNew.Domain.Common.Models;

namespace ProCardsNew.Domain.DeckAggregate.ValueObjects;

public sealed class DeckStatisticId: ValueObject
{
    public Guid Value { get; }

    private DeckStatisticId(Guid value)
    {
        Value = value;
    }

    public static DeckStatisticId CreateUnique()
    {
        return new(Guid.NewGuid());
    }
    
    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}