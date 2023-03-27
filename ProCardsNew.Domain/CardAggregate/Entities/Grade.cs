using ProCardsNew.Domain.CardAggregate.ValueObjects;
using ProCardsNew.Domain.Common.Models;
using ProCardsNew.Domain.DeckAggregate;
using ProCardsNew.Domain.DeckAggregate.ValueObjects;
using ProCardsNew.Domain.UserAggregate;
using ProCardsNew.Domain.UserAggregate.ValueObjects;

namespace ProCardsNew.Domain.CardAggregate.Entities;

public class Grade: Entity
{
    public CardId CardId { get; private set; }
    public DeckId DeckId { get; private set; }
    public UserId UserId { get; private set; }
    public Card? Card { get; private set; }
    public Deck? Deck { get; private set; }
    public User? User { get; private set; }
    public int GradeValue { get; private set; }
    public DateTime GradedAtDateTime { get; private set; }
    
    private Grade(
        CardId cardId,
        DeckId deckId,
        UserId userId,
        int gradeValue,
        DateTime gradedAtDateTime)
    {
        CardId = cardId;
        DeckId = deckId;
        UserId = userId;
        GradeValue = gradeValue;
        GradedAtDateTime = gradedAtDateTime;
    }

    public static Grade Create(
        CardId cardId,
        DeckId deckId,
        UserId userId,
        int gradeValue)
    {
        return new(
            cardId,
            deckId,
            userId,
            gradeValue,
            DateTime.UtcNow);
    }

    public override IEnumerable<object?> GetEqualityComponents()
    {
        yield return CardId;
        yield return DeckId;
        yield return UserId;
    }
    
#pragma warning disable CS8618
    // ReSharper disable once UnusedMember.Local
    private Grade()
    {
    }
#pragma warning restore CS8618
}