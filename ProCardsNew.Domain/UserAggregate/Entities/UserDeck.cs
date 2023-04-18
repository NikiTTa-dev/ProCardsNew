using ProCardsNew.Domain.Common.Models;
using ProCardsNew.Domain.DeckAggregate.Entities;
using ProCardsNew.Domain.DeckAggregate.ValueObjects;
using ProCardsNew.Domain.UserAggregate.ValueObjects;

namespace ProCardsNew.Domain.UserAggregate.Entities;

public class UserDeck: Entity
{
    public DeckAccessId DeckAccessId { get; private set; }
    public UserId UserId { get; private set; }
    public DeckAccess? DeckAccess { get; private set; }
    public User? User { get; private set; }
    public DateTime LastOpenedAtDateTime { get; private set; }
    
    private UserDeck(
        UserId userId,
        DeckAccessId deckAccessId,
        DateTime lastOpenedAtDateTime)
    {
        UserId = userId;
        DeckAccessId = deckAccessId;
        LastOpenedAtDateTime = lastOpenedAtDateTime;
    }

    public static UserDeck Create(UserId userId, DeckAccessId deckAccessId)
    {
        return new(
            userId,
            deckAccessId,
            DateTime.UtcNow);
    }

    public override IEnumerable<object?> GetEqualityComponents()
    {
        yield return UserId;
        yield return DeckAccessId;
        yield return LastOpenedAtDateTime;
    }
    
#pragma warning disable CS8618
    // ReSharper disable once UnusedMember.Local
    private UserDeck()
    {
    }
#pragma warning restore CS8618
}