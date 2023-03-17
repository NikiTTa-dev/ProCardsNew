using ProCardsNew.Domain.Common.Models;

namespace ProCardsNew.Domain.CardAggregate.ValueObjects;

public sealed class GradeId: ValueObject
{
    public Guid Value { get; }

    private GradeId(Guid value)
    {
        Value = value;
    }

    public static GradeId CreateUnique()
    {
        return new(Guid.NewGuid());
    }
    
    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}