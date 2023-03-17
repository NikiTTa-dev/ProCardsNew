using ProCardsNew.Domain.CardAggregate.ValueObjects;
using ProCardsNew.Domain.Common.Models;

namespace ProCardsNew.Domain.CardAggregate;

// TODO: Color

public sealed class Card: AggregateRoot<CardId>
{
    public string FrontSide { get; }
    public string BackSide { get; }
    //public string Color { get; }
    public DateTime CreatedAtDateTime { get; }
    public DateTime UpdatedAtDateTime { get; }

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
}