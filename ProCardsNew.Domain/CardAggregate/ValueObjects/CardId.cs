using ProCardsNew.Domain.Common.Models;

namespace ProCardsNew.Domain.CardAggregate.ValueObjects;

public sealed class CardId: ValueObject
{
    public Guid Value { get; }

    private CardId(Guid value)
    {
        Value = value;
    }

    public static CardId CreateUnique()
    {
        return new(Guid.NewGuid());
    }
    
    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}