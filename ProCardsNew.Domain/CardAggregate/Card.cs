using ProCardsNew.Domain.CardAggregate.ValueObjects;
using ProCardsNew.Domain.Common.Models;
using ProCardsNew.Domain.DeckAggregate;
using ProCardsNew.Domain.DeckAggregate.Entities;

namespace ProCardsNew.Domain.CardAggregate;

// TODO: Color

public sealed class Card: AggregateRoot<CardId>
{
    public string FrontSide { get; private set; }
    public string BackSide { get; private set; }
    //public string Color { get; private set; }
    public DateTime CreatedAtDateTime { get; private set; }
    public DateTime UpdatedAtDateTime { get; private set; }
    
    private readonly List<DeckCard> _deckCards = new();
    public IReadOnlyList<DeckCard> DeckCards => _deckCards.AsReadOnly();
    
    private readonly List<Deck> _decks = new();
    public IReadOnlyList<Deck> Decks => _decks.AsReadOnly();
    
    private Card(
        CardId id, 
        string frontSide,
        string backSide,
        //string color, 
        DateTime createdAtDateTime, 
        DateTime updatedAtDateTime)
        : base(id)
    {
        FrontSide = frontSide;
        BackSide = backSide;
        //Color = color;
        CreatedAtDateTime = createdAtDateTime;
        UpdatedAtDateTime = updatedAtDateTime;
    }

    public static Card Create(
        string frontSide,
        string backSide)
    {
        return new(
            CardId.CreateUnique(), 
            frontSide,
            backSide,
            DateTime.UtcNow,
            DateTime.UtcNow);
    }
    
#pragma warning disable CS8618
    private Card()
    {
        
    }
#pragma warning restore CS8618
}