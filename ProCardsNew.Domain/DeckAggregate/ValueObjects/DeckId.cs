using ProCardsNew.Domain.Common.Models;

namespace ProCardsNew.Domain.DeckAggregate.ValueObjects;

public sealed class DeckId: ValueObject
{
    public Guid Value { get; }

    private DeckId(Guid value)
    {
        Value = value;
    }

    public static DeckId CreateUnique()
    {
        return new(Guid.NewGuid());
    }

    public static DeckId Create(Guid value)
    {
        return new(value);
    }
    
    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}