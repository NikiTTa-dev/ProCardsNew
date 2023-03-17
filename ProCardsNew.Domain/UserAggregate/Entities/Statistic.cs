using ProCardsNew.Domain.Common.Models;
using ProCardsNew.Domain.UserAggregate.ValueObjects;

namespace ProCardsNew.Domain.UserAggregate.Entities;

public sealed class Statistic : Entity<StatisticId>
{
    public int CardsViewed { get; }
    public float Hours { get; }
    public int CardsCreated { get; }
    public int Score { get; }

    private Statistic(
        StatisticId id,
        int cardsViewed,
        float hours,
        int cardsCreated,
        int score)
        : base(id)
    {
        CardsViewed = cardsViewed;
        Hours = hours;
        CardsCreated = cardsCreated;
        Score = score;
    }

    public static Statistic Create()
    {
        return new(
            StatisticId.CreateUnique(),
            0,
            0, 
            0,
            0);
    }
}