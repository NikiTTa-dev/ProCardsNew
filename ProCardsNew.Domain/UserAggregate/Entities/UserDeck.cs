using ProCardsNew.Domain.Common.Models;
using ProCardsNew.Domain.DeckAggregate;
using ProCardsNew.Domain.DeckAggregate.ValueObjects;
using ProCardsNew.Domain.UserAggregate.ValueObjects;

namespace ProCardsNew.Domain.UserAggregate.Entities;

public class UserDeck: Entity
{
    public DeckId DeckId { get; private set; }
    public UserId UserId { get; private set; }
    public Deck? Deck { get; private set; }
    public User? User { get; private set; }
    public DateTime OpenedAtDateTime { get; private set; }
    public bool IsActive { get; private set; }
    
    private UserDeck(
        UserId userId,
        DeckId deckId,
        DateTime openedAtDateTime,
        bool isActive)
    {
        UserId = userId;
        DeckId = deckId;
        OpenedAtDateTime = openedAtDateTime;
        IsActive = isActive;
    }

    public static UserDeck Create(UserId userId, DeckId deckId)
    {
        return new(
            userId,
            deckId,
            DateTime.UtcNow, 
            true);
    }

    public override IEnumerable<object?> GetEqualityComponents()
    {
        yield return UserId;
        yield return DeckId;
    }
    
#pragma warning disable CS8618
    // ReSharper disable once UnusedMember.Local
    private UserDeck()
    {
    }
#pragma warning restore CS8618
}