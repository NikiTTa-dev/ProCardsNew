using ProCardsNew.Domain.CardAggregate.ValueObjects;
using ProCardsNew.Domain.Common.Models;

namespace ProCardsNew.Domain.CardAggregate.Entities;

public sealed class Image : Entity<ImageId>
{
    public CardId CardId { get; private set; }
    public Card? Card { get; private set; }
    public string Name { get; private set; }
    public string FileExtension { get; private set; }
    public ImageData? ImageData { get; private set; }
    public DateTime CreatedAt { get; private set; }
    private readonly List<Card> _cardsWithFrontSide = new();
    public IReadOnlyList<Card> CardsWithFrontSide => _cardsWithFrontSide.AsReadOnly();
    private readonly List<Card> _cardsWithBackSide = new();
    public IReadOnlyList<Card> CardsWithBackSide => _cardsWithBackSide.AsReadOnly();

    private Image(
        ImageId id,
        CardId cardId,
        string name,
        string fileExtension,
        ImageData data,
        DateTime createdAt)
        : base(id)
    {
        ImageData = data;
        CardId = cardId;
        Name = name;
        FileExtension = fileExtension;
        CreatedAt = createdAt;
    }

    public static Image Create(
        CardId cardId,
        string name,
        string fileExtension,
        ImageData data)
    {
        return new(
            ImageId.CreateUnique(),
            cardId,
            name,
            fileExtension,
            data,
            DateTime.UtcNow);
    }

    public override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Id;
    }

#pragma warning disable CS8618
    // ReSharper disable once UnusedMember.Local
    private Image()
    {
    }
#pragma warning restore CS8618
}