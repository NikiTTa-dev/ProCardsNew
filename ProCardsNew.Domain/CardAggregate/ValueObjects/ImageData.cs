using ProCardsNew.Domain.CardAggregate.Entities;
using ProCardsNew.Domain.Common.Models;

namespace ProCardsNew.Domain.CardAggregate.ValueObjects;

public class ImageData: ValueObject
{
    public Image? Image { get; private set; }
    public ImageId? ImageId { get; private set; }
    public byte[] Data { get; private set; }

    private ImageData(
        byte[] data)
    {
        Data = data;
    }

    public static ImageData Create(byte[] data)
    {
        return new ImageData(data);
    }

    public override IEnumerable<object?> GetEqualityComponents()
    {
        yield return ImageId;
    }
}