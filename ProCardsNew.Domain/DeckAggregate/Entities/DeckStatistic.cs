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
        UpdateScore(1, 360);
        return this;
    }

    private void UpdateScore(
        float cardsViewedCoefficient,
        float hoursCoefficient)
    {
        var average = (CardsViewed * cardsViewedCoefficient
                       + (int)(Hours * hoursCoefficient))
                      / 2;
        var normalizedCardsViewedCoefficient = cardsViewedCoefficient;
        var normalizedHoursCoefficient = hoursCoefficient;
        
        if (cardsViewedCoefficient * CardsViewed > 1.2f * average)
        {
            normalizedCardsViewedCoefficient *=
                Math.Min(1f, 1f / (2f * average / (Hours * hoursCoefficient)));
        }

        if (hoursCoefficient * Hours > 1.2f * average)
        {
            normalizedHoursCoefficient *= Math.Min(1f,
                1f / (2f * average / (CardsViewed * cardsViewedCoefficient)));
        }

        Score = (int)(CardsViewed * normalizedCardsViewedCoefficient
                      + Hours * normalizedHoursCoefficient);
    }

#pragma warning disable CS8618
    // ReSharper disable once UnusedMember.Local
    private DeckStatistic()
    {
    }
#pragma warning restore CS8618
}