using ProCardsNew.Domain.CardAggregate;
using ProCardsNew.Domain.Common.Models;
using ProCardsNew.Domain.DeckAggregate.Entities;
using ProCardsNew.Domain.DeckAggregate.ValueObjects;
using ProCardsNew.Domain.UserAggregate;
using ProCardsNew.Domain.UserAggregate.ValueObjects;

namespace ProCardsNew.Domain.DeckAggregate;

public sealed class Deck : AggregateRoot<DeckId>
{
    public string Name { get; private set; }
    public string Description { get; private set; }
    public string? PasswordHash { get; private set; }
    public bool IsPublic { get; private set; }
    public UserId OwnerId { get; private set; }
    public User? Owner { get; private set; }
    public DateTime CreatedAtDateTime { get; private set; }
    public DateTime UpdatedAtDateTime { get; private set; }


    private readonly List<DeckCard> _deckCards = new();
    public IReadOnlyList<DeckCard> DeckCards => _deckCards.AsReadOnly();
    private readonly List<Card> _cards = new();
    public IReadOnlyList<Card> Cards => _cards.AsReadOnly();

    private readonly List<DeckStatistic> _deckStatistics = new();
    public IReadOnlyList<DeckStatistic> DeckStatistics => _deckStatistics.AsReadOnly();
    private readonly List<User> _leaderboardUsers = new();
    public IReadOnlyList<User> LeaderboardUsers => _leaderboardUsers.AsReadOnly();

    private readonly List<DeckAccess> _deckAccesses = new();
    public IReadOnlyList<DeckAccess> DeckAccesses => _deckAccesses.AsReadOnly();

    private Deck(
        DeckId id,
        string name,
        string description,
        UserId ownerId,
        DateTime createdAtDateTime,
        DateTime updatedAtDateTime)
        : base(id)
    {
        OwnerId = ownerId;
        Name = name;
        Description = description;
        CreatedAtDateTime = createdAtDateTime;
        UpdatedAtDateTime = updatedAtDateTime;
    }

    public static Deck Create(
        string name,
        string description,
        UserId ownerId)
    {
        var deck = new Deck(
            DeckId.CreateUnique(),
            name,
            description,
            ownerId,
            DateTime.UtcNow,
            DateTime.UtcNow);
        deck._deckAccesses.Add(DeckAccess.Create(deck.Id, false));
        return deck;
    }

    public DeckCard AddCard(Card card)
    {
        var deckCard = DeckCard.Create(card.Id, Id);
        _deckCards.Add(deckCard);
        
        return deckCard;
    }

    public void Edit(
        string name,
        string description)
    {
        Name = name;
        Description = description;
        UpdatedAtDateTime = DateTime.UtcNow;
    }

    public void EditPassword(string? passwordHash)
    {
        PasswordHash = passwordHash;
        UpdatedAtDateTime = DateTime.UtcNow;
    }

    public void OpenDeck()
    {
        IsPublic = true;
        _deckAccesses.Add(DeckAccess.Create(Id, true));
        UpdatedAtDateTime = DateTime.UtcNow;
    }

    public void CloseDeck(DeckAccess? deckAccess)
    {
        IsPublic = false;
        deckAccess?.Close();
        UpdatedAtDateTime = DateTime.UtcNow;
    }

    public void AddStatistic(UserId userId)
    {
        _deckStatistics.Add(DeckStatistic.Create(Id, userId));
    }

#pragma warning disable CS8618
    // ReSharper disable once UnusedMember.Local
    private Deck()
    {
    }
#pragma warning restore CS8618
}