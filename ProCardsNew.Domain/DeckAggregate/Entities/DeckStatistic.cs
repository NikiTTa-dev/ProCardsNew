using ProCardsNew.Domain.Common.Models;
using ProCardsNew.Domain.DeckAggregate.ValueObjects;

namespace ProCardsNew.Domain.DeckAggregate.Entities;

public sealed class DeckStatistic: Entity<DeckStatisticId>
{
    public int CardsViewed { get; }
    public float Hours { get; }
    public int Score { get; } 
    
    private DeckStatistic(
        DeckStatisticId id,
        int cardsViewed,
        float hours,
        int score)
        : base(id)
    {
        CardsViewed = cardsViewed;
        Hours = hours;
        Score = score;
    }

    public static DeckStatistic Create()
    {
        return new(
            DeckStatisticId.CreateUnique(),
            0,
            0,
            0);
    }
}