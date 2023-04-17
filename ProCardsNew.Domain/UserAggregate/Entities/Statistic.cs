using ProCardsNew.Domain.Common.Models;
using ProCardsNew.Domain.UserAggregate.ValueObjects;

namespace ProCardsNew.Domain.UserAggregate.Entities;

public sealed class Statistic : Entity<UserId>
{
    public User? User { get; private set; }
    public int CardsViewed { get; private set; }
    public float Hours { get; private set; }
    public int CardsCreated { get; private set; }
    public int Score { get; private set; }

    private Statistic(UserId id) : base(id)
    {
    }

    public void IncreaseCardsCreated()
    {
        CardsCreated++;
        UpdateScore(1, 5, 360);
    }

    private void UpdateScore(
        float cardsViewedCoefficient,
        float cardsCreatedCoefficient,
        float hoursCoefficient)
    {
        var average = (CardsViewed * cardsViewedCoefficient
                       + CardsCreated * cardsCreatedCoefficient
                       + (int)(Hours * hoursCoefficient))
                      / 3;
        var normalizedCardsViewedCoefficient = cardsViewedCoefficient;
        var normalizedCardsCreatedCoefficient = cardsCreatedCoefficient;
        var normalizedHoursCoefficient = hoursCoefficient;
        
        if (cardsViewedCoefficient * CardsViewed > 1.2f * average)
        {
            normalizedCardsViewedCoefficient *=
                Math.Min(1f, 1f /
                             (3f * average /
                              (CardsCreated * cardsCreatedCoefficient
                               + Hours * hoursCoefficient)));
        }

        if (cardsCreatedCoefficient * CardsCreated > 1.2f * average)
        {
            normalizedCardsCreatedCoefficient *=
                Math.Min(1f,
                    1f /
                    (3f * average /
                     (CardsViewed * cardsViewedCoefficient +
                      Hours * hoursCoefficient)));
        }
        
        if (hoursCoefficient * Hours > 1.2f * average)
        {
            normalizedHoursCoefficient *= Math.Min(1f,
                1f /
                (3f * average /
                 (CardsCreated * cardsCreatedCoefficient +
                  CardsViewed * cardsViewedCoefficient)));
        }

        Score = (int)(CardsViewed * normalizedCardsViewedCoefficient
                      + CardsCreated * normalizedCardsCreatedCoefficient
                      + Hours * normalizedHoursCoefficient);
    }

    public static Statistic Create(UserId userId)
    {
        return new(userId);
    }
}