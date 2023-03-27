using ProCardsNew.Domain.Common.Models;

namespace ProCardsNew.Domain.UserAggregate.ValueObjects;

public sealed class RefreshToken: ValueObject
{
    public string Value { get; }

    private RefreshToken(string value)
    {
        Value = value;
    }

    public static RefreshToken CreateUnique()
    {
        return new(Guid.NewGuid().ToString());
    }
    public static RefreshToken Create(string value)
    {
        return new(value);
    }
    
    
    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}