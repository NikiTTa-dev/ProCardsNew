using ProCardsNew.Domain.Common.Models;

namespace ProCardsNew.Domain.DeckAggregate.ValueObjects;

public sealed class DeckCardId: ValueObject
{
    public Guid Value { get; }

    private DeckCardId(Guid value)
    {
        Value = value;
    }

    public static DeckCardId CreateUnique()
    {
        return new(Guid.NewGuid());
    }
    
    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}