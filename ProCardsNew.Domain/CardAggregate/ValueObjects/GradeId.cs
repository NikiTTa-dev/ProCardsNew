using ProCardsNew.Domain.Common.Models;

namespace ProCardsNew.Domain.CardAggregate.ValueObjects;

public sealed class GradeId: ValueObject
{
    public Guid Value { get; }
    public Guid Guid { get; set; }

    private GradeId(Guid value)
    {
        Value = value;
    }

    public static GradeId CreateUnique()
    {
        return new(Guid.NewGuid());
    }

    public static GradeId Create(Guid value)
    {
        return new(value);
    }
    
    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}