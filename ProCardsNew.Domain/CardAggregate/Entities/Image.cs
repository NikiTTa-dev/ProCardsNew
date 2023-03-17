using ProCardsNew.Domain.CardAggregate.ValueObjects;
using ProCardsNew.Domain.Common.Models;

namespace ProCardsNew.Domain.CardAggregate.Entities;

public sealed class Image: Entity<ImageId>
{
    public string Name { get; }
    public string FileExtension { get; }
    public string Data { get; }
    public DateTime UpdatedAt { get; }
    
    private Image(
        ImageId id,
        string name,
        string fileExtension, 
        string data,
        DateTime updatedAt)
        : base(id)
    {
        Name = name;
        FileExtension = fileExtension;
        Data = data;
        UpdatedAt = updatedAt;
    }

    public static Image Create(
        string name,
        string fileExtension,
        string data)
    {
        return new(
            ImageId.CreateUnique(), 
            name,
            fileExtension,
            data,
            DateTime.UtcNow);
    }
}