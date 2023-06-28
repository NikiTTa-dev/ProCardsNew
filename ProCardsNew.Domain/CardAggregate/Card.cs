using ProCardsNew.Domain.CardAggregate.Entities;
using ProCardsNew.Domain.CardAggregate.ValueObjects;
using ProCardsNew.Domain.Common.Models;
using ProCardsNew.Domain.DeckAggregate;
using ProCardsNew.Domain.DeckAggregate.Entities;
using ProCardsNew.Domain.DeckAggregate.ValueObjects;
using ProCardsNew.Domain.UserAggregate;
using ProCardsNew.Domain.UserAggregate.ValueObjects;

namespace ProCardsNew.Domain.CardAggregate;

// TODO: Color IMAGES

public sealed class Card: AggregateRoot<CardId>
{
    public string FrontSide { get; private set; }
    public string BackSide { get; private set; }
    //public string Color { get; private set; }
    public UserId OwnerId { get; private set; }
    public User? Owner { get; private set; }
    public DateTime UpdatedAtDateTime { get; private set; }
    public DateTime CreatedAtDateTime { get; private set; }
    //public Image FrontImage { get; private set; }
    //public Image BackImage { get; private set; }

    private readonly List<DeckCard> _deckCards = new();
    public IReadOnlyList<DeckCard> DeckCards => _deckCards.AsReadOnly();
    private readonly List<Deck> _decks = new();
    public IReadOnlyList<Deck> Decks => _decks.AsReadOnly();
    
    private readonly List<Grade> _grades = new();
    public IReadOnlyList<Grade> Grades => _grades.AsReadOnly();

    private readonly List<Image> _images = new();
    public IReadOnlyList<Image> Images => _images.AsReadOnly();

    private Card(
        CardId id, 
        string frontSide,
        string backSide,
        UserId ownerId,
        //string color, 
        DateTime createdAtDateTime, 
        DateTime updatedAtDateTime)
        : base(id)
    {
        FrontSide = frontSide;
        BackSide = backSide;
        OwnerId = ownerId;
        //Color = color;
        CreatedAtDateTime = createdAtDateTime;
        UpdatedAtDateTime = updatedAtDateTime;
    }

    public static Card Create(
        UserId ownerId,
        string frontSide,
        string backSide)
    {
        return new(
            CardId.CreateUnique(), 
            frontSide,
            backSide,
            ownerId,
            DateTime.UtcNow,
            DateTime.UtcNow);
    }

    public void Edit(
        string frontSide,
        string backSide)
    {
        FrontSide = frontSide;
        BackSide = backSide;
        UpdatedAtDateTime = DateTime.UtcNow;
    }

    public void AddImage(Image image)
    {
        _images.Add(image);
        UpdatedAtDateTime = DateTime.UtcNow;
    }

    public void GradeCard(UserId userId, DeckId deckId, int grade)
    {
        _grades.Add(Grade.Create(Id, deckId, userId, grade));
    }
    
#pragma warning disable CS8618
    // ReSharper disable once UnusedMember.Local
    private Card()
    {
        
    }
#pragma warning restore CS8618
}