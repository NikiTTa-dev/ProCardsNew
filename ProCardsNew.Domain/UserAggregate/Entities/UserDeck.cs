using ProCardsNew.Domain.Common.Models;
using ProCardsNew.Domain.UserAggregate.ValueObjects;

namespace ProCardsNew.Domain.UserAggregate.Entities;

public class UserDeck: Entity<UserDeckId>
{
    public DateTime OpenedAtDateTime { get; }
    public bool IsActive { get; }
    
    private UserDeck(
        UserDeckId id,
        DateTime openedAtDateTime,
        bool isActive)
        : base(id)
    {
        OpenedAtDateTime = openedAtDateTime;
        IsActive = isActive;
    }

    public static UserDeck Create()
    {
        return new(
            UserDeckId.CreateUnique(), 
            DateTime.UtcNow, 
            true);
    }
}