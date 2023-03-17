using ProCardsNew.Domain.Common.Models;
using ProCardsNew.Domain.DeckAggregate.ValueObjects;

namespace ProCardsNew.Domain.DeckAggregate.Entities;

public class DeckCard: Entity<DeckCardId>
{
    public DateTime AddedAt { get; }
    
    private DeckCard(
        DeckCardId id,
        DateTime addedAt)
        : base(id)
    {
        AddedAt = addedAt;
    }

    public static DeckCard Create()
    {
        return new(
            DeckCardId.CreateUnique(),
            DateTime.UtcNow);
    }
}