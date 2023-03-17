using ProCardsNew.Domain.Common.Models;

namespace ProCardsNew.Domain.CardAggregate.ValueObjects;

public sealed class ImageId: ValueObject
{
    public Guid Value { get; }

    private ImageId(Guid value)
    {
        Value = value;
    }

    public static ImageId CreateUnique()
    {
        return new(Guid.NewGuid());
    }
    
    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}