using ProCardsNew.Domain.Common.Models;
using ProCardsNew.Domain.DeckAggregate.ValueObjects;
using ProCardsNew.Domain.UserAggregate;
using ProCardsNew.Domain.UserAggregate.ValueObjects;

namespace ProCardsNew.Domain.DeckAggregate.Entities;

public sealed class DeckStatistic : Entity
{
    public DeckId DeckId { get; private set; }
    public UserId UserId { get; private set; }
    public Deck? Deck { get; private set; }
    public User? User { get; private set; }
    public int CardsViewed { get; private set; }
    public float Hours { get; private set; }
    public int Score { get; private set; }


    private DeckStatistic(
        DeckId deckId,
        UserId userId,
        int cardsViewed,
        float hours,
        int score)
    {
        DeckId = deckId;
        UserId = userId;
        CardsViewed = cardsViewed;
        Hours = hours;
        Score = score;
    }

    public static DeckStatistic Create(DeckId deckId, UserId userId)
    {
        return new(
            deckId,
            userId,
            0,
            0,
            0);
    }

    public override IEnumerable<object?> GetEqualityComponents()
    {
        yield return DeckId;
        yield return UserId;
    }

    public DeckStatistic IncreaseStatistic(int cardsViewed, float hours)
    {
        CardsViewed += cardsViewed;
        Hours += hours;
        return this;
    }

#pragma warning disable CS8618
    // ReSharper disable once UnusedMember.Local
    private DeckStatistic()
    {
    }
#pragma warning restore CS8618
}