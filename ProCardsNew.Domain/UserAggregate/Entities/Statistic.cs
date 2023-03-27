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

    public static Statistic Create(UserId userId)
    {
        return new(userId);
    }
}