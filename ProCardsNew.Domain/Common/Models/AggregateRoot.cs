namespace ProCardsNew.Domain.Common.Models;

public class AggregateRoot<TId>: Entity<TId>
    where TId: notnull
{
    public AggregateRoot(TId id) : base(id)
    {
    }
}