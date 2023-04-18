using ProCardsNew.Domain.Common.Models;

namespace ProCardsNew.Domain.DeckAggregate.ValueObjects;

public sealed class DeckAccessId: ValueObject
{
    public Guid Value { get; }

    private DeckAccessId(Guid value)
    {
        Value = value;
    }

    public static DeckAccessId CreateUnique()
    {
        return new(Guid.NewGuid());
    }

    public static DeckAccessId Create(Guid value)
    {
        return new(value);
    }
    
    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}