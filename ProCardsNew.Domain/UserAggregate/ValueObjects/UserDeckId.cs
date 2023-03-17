using ProCardsNew.Domain.Common.Models;

namespace ProCardsNew.Domain.UserAggregate.ValueObjects;

public sealed class UserDeckId: ValueObject
{
    public Guid Value { get; }

    private UserDeckId(Guid value)
    {
        Value = value;
    }

    public static UserDeckId CreateUnique()
    {
        return new(Guid.NewGuid());
    }
    
    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}