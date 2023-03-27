using ProCardsNew.Domain.CardAggregate.ValueObjects;
using ProCardsNew.Domain.Common.Models;

namespace ProCardsNew.Domain.CardAggregate.Entities;

public sealed class Image: Entity
{
    public CardId CardId { get; private set; }
    public SideId SideId { get; private set; }
    public Card? Card { get; private set; }
    public Side? Side { get; private set; }
    public string Name { get; private set; }
    public string FileExtension { get; private set; }
    public string Data { get; private set; }
    public DateTime UpdatedAt { get; private set; }
    
    private Image(
        CardId cardId,
        SideId sideId,
        string name,
        string fileExtension, 
        string data,
        DateTime updatedAt)
    {
        CardId = cardId;
        SideId = sideId;
        Name = name;
        FileExtension = fileExtension;
        Data = data;
        UpdatedAt = updatedAt;
    }

    public static Image Create(
        CardId cardId,
        SideId sideId,
        string name,
        string fileExtension,
        string data)
    {
        return new(
            cardId,
            sideId,
            name,
            fileExtension,
            data,
            DateTime.UtcNow);
    }

    public override IEnumerable<object?> GetEqualityComponents()
    {
        yield return CardId;
        yield return SideId;
    }
    
#pragma warning disable CS8618
    // ReSharper disable once UnusedMember.Local
    private Image()
    {
    }
#pragma warning restore CS8618
}