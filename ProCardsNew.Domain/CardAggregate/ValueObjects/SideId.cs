using ProCardsNew.Domain.Common.Models;

namespace ProCardsNew.Domain.CardAggregate.ValueObjects;

public sealed class SideId: ValueObject
{
    public Guid Value { get; }

    private SideId(Guid value)
    {
        Value = value;
    }

    public static SideId CreateUnique()
    {
        return new(Guid.NewGuid());
    }

    public static SideId Create(Guid value)
    {
        return new(value);
    }
    
    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}