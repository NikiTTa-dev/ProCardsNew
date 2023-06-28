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

    public static ImageId Create(Guid value)
    {
        return new(value);
    }
    
    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}