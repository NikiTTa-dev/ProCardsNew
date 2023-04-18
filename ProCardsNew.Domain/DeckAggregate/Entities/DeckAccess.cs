using ProCardsNew.Domain.Common.Models;
using ProCardsNew.Domain.DeckAggregate.ValueObjects;
using ProCardsNew.Domain.UserAggregate.Entities;
using ProCardsNew.Domain.UserAggregate.ValueObjects;

namespace ProCardsNew.Domain.DeckAggregate.Entities;

public sealed class DeckAccess : Entity<DeckAccessId>
{
    public DeckId DeckId { get; private set; }
    public Deck? Deck { get; private set; }
    private readonly List<UserDeck> _userDecks = new ();
    public IReadOnlyList<UserDeck> UserDecks => _userDecks.AsReadOnly();
    public bool IsAccessible { get; private set; }

    private DeckAccess(
        DeckAccessId id,
        DeckId deckId,
        bool isAccessible)
        : base(id)
    {
        DeckId = deckId;
        IsAccessible = isAccessible;
    }

    public static DeckAccess Create(
        DeckId deckId,
        bool isAccessible)
    {
        return new(
            DeckAccessId.CreateUnique(),
            deckId,
            isAccessible);
    }

    public void Close()
    {
        IsAccessible = false;
    }

    public void AddUser(
        UserId user)
    {
        _userDecks.Add(UserDeck.Create(user, Id));
    }
}