using ProCardsNew.Domain.Common.Models;
using ProCardsNew.Domain.DeckAggregate.ValueObjects;

namespace ProCardsNew.Domain.DeckAggregate;

public sealed class Deck: AggregateRoot<DeckId>
{
    public string Name { get; }
    public string Description { get; }
    public string? PasswordHash { get; }
    public bool IsPublic { get; }
    public int CardsCount { get; }
    public DateTime CreatedAtDateTime { get; }
    public DateTime UpdatedAtDateTime { get; }

    private Deck(
        DeckId id,
        string name,
        string description,
        string passwordHash,
        bool isPublic,
        int cardsCount,
        DateTime createdAtDateTime,
        DateTime updatedAtDateTime)
        : base(id)
    {
        Name = name;
        Description = description;
        PasswordHash = passwordHash;
        IsPublic = isPublic;
        CardsCount = cardsCount;
        CreatedAtDateTime = createdAtDateTime;
        UpdatedAtDateTime = updatedAtDateTime;
    }

    public static Deck Create(
        string name,
        string description,
        bool isPublic,
        string passwordHash)
    {
        return new(
            DeckId.CreateUnique(),
            name,
            description,
            passwordHash,
            isPublic,
            0,
            DateTime.UtcNow,
            DateTime.UtcNow);
    }
}