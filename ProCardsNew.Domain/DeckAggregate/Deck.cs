using ProCardsNew.Domain.CardAggregate;
using ProCardsNew.Domain.Common.Models;
using ProCardsNew.Domain.DeckAggregate.Entities;
using ProCardsNew.Domain.DeckAggregate.ValueObjects;
using ProCardsNew.Domain.UserAggregate;
using ProCardsNew.Domain.UserAggregate.Entities;

namespace ProCardsNew.Domain.DeckAggregate;

public sealed class Deck: AggregateRoot<DeckId>
{
    public string Name { get; private set; }
    public string Description { get; }
    public string? PasswordHash { get; private set; }
    public bool IsPublic { get; private set; }
    public int CardsCount { get; private set; }
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
    
    private readonly List<UserDeck> _userDecks = new();
    public IReadOnlyList<UserDeck> UserDecks => _userDecks.AsReadOnly();
    private readonly List<User> _users = new();
    public IReadOnlyList<User> Users => _users.AsReadOnly(); 
    
    private Deck(
        DeckId id,
        string name,
        string description,
        string passwordHash,
        bool isPublic,
        int cardsCount,
        DateTime createdAtDateTime,
        DateTime updatedAtDateTime)
        : base(id)
    {
        Name = name;
        Description = description;
        PasswordHash = passwordHash;
        IsPublic = isPublic;
        CardsCount = cardsCount;
        CreatedAtDateTime = createdAtDateTime;
        UpdatedAtDateTime = updatedAtDateTime;
    }

    public static Deck Create(
        string name,
        string description,
        bool isPublic,
        string passwordHash)
    {
        return new(
            DeckId.CreateUnique(),
            name,
            description,
            passwordHash,
            isPublic,
            0,
            DateTime.UtcNow,
            DateTime.UtcNow);
    }

    public DeckCard AddCard(Card card)
    {
        _cards.Add(card);
        var deckCard = DeckCard.Create(card.Id, Id);
        _deckCards.Add(deckCard);
        CardsCount++;
        
        return deckCard;
    }

    public DeckStatistic AddStatistic(User user)
    {
        var deckStatistic = DeckStatistic.Create(Id, user.Id);
        _deckStatistics.Add(deckStatistic);

        return deckStatistic;
    }
    
#pragma warning disable CS8618
    // ReSharper disable once UnusedMember.Local
    private Deck()
    {
        
    }
#pragma warning restore CS8618
}