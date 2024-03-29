﻿using ProCardsNew.Domain.CardAggregate;
using ProCardsNew.Domain.Common.Models;
using ProCardsNew.Domain.DeckAggregate;
using ProCardsNew.Domain.DeckAggregate.Entities;
using ProCardsNew.Domain.UserAggregate.Entities;
using ProCardsNew.Domain.UserAggregate.Enums;
using ProCardsNew.Domain.UserAggregate.ValueObjects;

namespace ProCardsNew.Domain.UserAggregate;

public sealed class User : AggregateRoot<UserId>
{
    public string Login { get; private set; }
    public string Email { get; private set; }
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public string Location { get; private set; }
    public int AvatarNumber { get; private set; }
    public RefreshToken? RefreshToken { get; private set; }
    public string? PasswordRecoveryCode { get; private set; }
    public DateTime? PasswordRecoveryEndDateTime { get; private set; }
    public int PasswordRecoveryFailedCount { get; private set; }
    public DateTime? PasswordRecoveryLastEmailSentDateTime { get; private set; }
    public string PasswordHash { get; private set; }
    public int AccessFailedCount { get; private set; }
    public DateTime? LockoutEndDateTime { get; private set; }
    public DateTime CreatedAtDateTime { get; private set; }
    public DateTime UpdatedAtDateTime { get; private set; }
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

    private User(
        UserId id,
        string login,
        string email,
        string firstName,
        string lastName,
        string location,
        string passwordHash,
        Statistic statistic,
        DateTime createdAtDateTime,
        DateTime updatedAtDateTime)
        : base(id)
    {
        Login = login;
        Email = email;
        FirstName = firstName;
        LastName = lastName;
        Location = location;
        PasswordHash = passwordHash;
        Statistic = statistic;
        CreatedAtDateTime = createdAtDateTime;
        UpdatedAtDateTime = updatedAtDateTime;
    }

    public static User Create(
        string login,
        string email,
        string firstName,
        string lastName,
        string location,
        string passwordHash)
    {
        var userId = UserId.CreateUnique();
        return new(
            userId,
            login,
            email,
            firstName,
            lastName,
            location,
            passwordHash,
            Statistic.Create(userId),
            DateTime.UtcNow,
            DateTime.UtcNow);
    }

    public void AddOwnedDeck(Deck deck)
    {
        _ownedDecks.Add(deck);
        _userDecks.Add(UserDeck.Create(Id, deck.DeckAccesses.First().Id));
        _deckStatistics.Add(DeckStatistic.Create(deck.Id, Id));
    }

    public RefreshToken GenerateRefreshToken()
    {
        RefreshToken = RefreshToken.CreateUnique();
        return RefreshToken;
    }

    public void Edit(
        string firstName,
        string lastName,
        string email,
        string location,
        int avatarNumber)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        Location = location;
        AvatarNumber = avatarNumber;

        UpdatedAtDateTime = DateTime.UtcNow;
    }

    public void RehashPassword(string newPasswordHash)
    {
        PasswordHash = newPasswordHash;
        UpdatedAtDateTime = DateTime.UtcNow;
    }

    public void SetPasswordRecoveryCode(
        string recoveryCode,
        int expirationMinutes)
    {
        PasswordRecoveryCode = recoveryCode;
        PasswordRecoveryEndDateTime = DateTime.UtcNow.AddMinutes(expirationMinutes);
    }

    public void SetEmailSendingTimeout()
    {
        PasswordRecoveryLastEmailSentDateTime = DateTime.UtcNow;
    }

    public void DeletePasswordRecoveryCode()
    {
        PasswordRecoveryCode = null;
        PasswordRecoveryFailedCount = 0;
    }

    public void PasswordRecoveryFail(int passwordRecoveryFailedMaxCountInclusive)
    {
        PasswordRecoveryFailedCount++;
        if (PasswordRecoveryFailedCount < passwordRecoveryFailedMaxCountInclusive)
            return;
        PasswordRecoveryCode = null;
        PasswordRecoveryFailedCount = 0;
    }

    public AccessFailResult AccessFail(
        int lockoutMinutes,
        int accessFailedMaxCountInclusive)
    {
        AccessFailedCount++;
        if (AccessFailedCount < accessFailedMaxCountInclusive)
            return AccessFailResult.AccessFailedCounterIncreased;

        LockoutUserLogin(lockoutMinutes);
        return AccessFailResult.LockedOut;
    }

    public bool IsLockedOut()
    {
        return LockoutEndDateTime > DateTime.UtcNow;
    }

    public void ResetAccessFailsCount()
    {
        AccessFailedCount = 0;
    }

    private void LockoutUserLogin(int lockoutMinutes)
    {
        ResetAccessFailsCount();
        LockoutEndDateTime = DateTime.UtcNow.AddMinutes(lockoutMinutes);
    }

#pragma warning disable CS8618
    // ReSharper disable once UnusedMember.Local
    private User()
    {
    }
#pragma warning restore CS8618
}