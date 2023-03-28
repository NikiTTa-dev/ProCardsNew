using ProCardsNew.Domain.CardAggregate;
using ProCardsNew.Domain.Common.Models;
using ProCardsNew.Domain.DeckAggregate;
using ProCardsNew.Domain.DeckAggregate.Entities;
using ProCardsNew.Domain.UserAggregate.Entities;
using ProCardsNew.Domain.UserAggregate.ValueObjects;

namespace ProCardsNew.Domain.UserAggregate;

public sealed class User: AggregateRoot<UserId>
{
    public string Login { get; private set;}
    public string Email { get; private set;}
    public string NormalizedLogin { get; private set;} 
    public string NormalizedEmail { get; private set;}
    public string FirstName { get; private set;}
    public string LastName { get; private set;}
    public string Location { get; private set;}
    public RefreshToken? RefreshToken { get; private set;}
    public string? PasswordRecoveryCode { get; private set;} = null;
    public DateTime? PasswordRecoveryEndDateTime { get; private set;} = null;
    public string PasswordHash { get; private set;}
    public int AccessFailedCount { get; private set;}
    public DateTime? LockoutEndDateTime { get; private set;} = null;
    public DateTime CreatedAtDateTime { get; private set;}
    public DateTime UpdatedAtDateTime { get; private set;}
    public Statistic? Statistic { get; private set; }

    private readonly List<Deck> _ownedDecks = new();
    public IReadOnlyList<Deck> OwnedDecks => _ownedDecks.AsReadOnly();
    
    private readonly List<Card> _ownedCards = new();
    public IReadOnlyList<Card> OwnedCards => _ownedCards.AsReadOnly();

    private readonly List<DeckStatistic> _deckStatistics = new();
    public IReadOnlyList<DeckStatistic> DeckStatistics => _deckStatistics.AsReadOnly();
    private readonly List<Deck> _leaderboardWithUserDecks = new();
    public IReadOnlyList<Deck> LeaderboardWithUserDecks => _leaderboardWithUserDecks.AsReadOnly();
    
    private readonly List<UserDeck> _userDecks = new();
    public IReadOnlyList<UserDeck> UserDecks => _userDecks.AsReadOnly();
    private readonly List<Deck> _decks = new();
    public IReadOnlyList<Deck> Decks => _decks.AsReadOnly();

    private User(
        UserId id,
        string login,
        string email,
        string normalizedLogin,
        string normalizedEmail,
        string firstName,
        string lastName,
        string location,
        string passwordHash,
        int accessFailedCount,
        DateTime createdAtDateTime,
        DateTime updatedAtDateTime)
        : base(id)
    {
        Login = login;
        Email = email;
        NormalizedLogin = normalizedLogin;
        NormalizedEmail = normalizedEmail;
        FirstName = firstName;
        LastName = lastName;
        Location = location;
        PasswordHash = passwordHash;
        CreatedAtDateTime = createdAtDateTime;
        UpdatedAtDateTime = updatedAtDateTime;
        AccessFailedCount = accessFailedCount;
    }

    public static User Create(
        string login,
        string email,
        string firstName,
        string lastName,
        string location,
        string passwordHash)
    {
        return new(
            UserId.CreateUnique(),
            login,
            email,
            login.ToUpper(),
            email.ToUpper(),
            firstName,
            lastName,
            location,
            passwordHash,
            0,
            DateTime.UtcNow, 
            DateTime.UtcNow);
    }

    public RefreshToken GenerateRefreshToken()
    {
        RefreshToken = RefreshToken.CreateUnique();
        return RefreshToken;
    }

    public string RehashPassword(string newPasswordHash)
    {
        PasswordHash = newPasswordHash;
        
        return PasswordHash;
    }
    
#pragma warning disable CS8618
    // ReSharper disable once UnusedMember.Local
    private User()
    {
    }
#pragma warning restore CS8618
}