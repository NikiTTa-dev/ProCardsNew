using ProCardsNew.Domain.CardAggregate;
using ProCardsNew.Domain.CardAggregate.ValueObjects;
using ProCardsNew.Domain.Common.Models;
using ProCardsNew.Domain.DeckAggregate.ValueObjects;

namespace ProCardsNew.Domain.DeckAggregate.Entities;

public class DeckCard: Entity
{
    public Card? Card { get; private set; }
    public Deck? Deck { get; private set; }
    public CardId CardId { get; private set; }
    public DeckId DeckId { get; private set; }
    public DateTime AddedAt { get; private set; }
    
    private DeckCard(
        CardId card,
        DeckId deck,
        DateTime addedAt)
    {
        CardId = card;
        DeckId = deck;
        AddedAt = addedAt;
    }

    public static DeckCard Create(CardId cardId, DeckId deckId)
    {
        return new(
            cardId,
            deckId,
            DateTime.UtcNow);
    }
    
    public override IEnumerable<object?> GetEqualityComponents()
    {
        yield return CardId;
        yield return DeckId;
    }

#pragma warning disable CS8618
    // ReSharper disable once UnusedMember.Local
    private DeckCard()
    {
    }
#pragma warning restore CS8618
}