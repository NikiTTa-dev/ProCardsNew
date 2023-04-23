using ProCardsNew.Domain.CardAggregate.Entities;
using ProCardsNew.Domain.CardAggregate.ValueObjects;

namespace ProCardsNew.Application.Common.Interfaces.Persistence;

public interface IImageRepository
{
    public Task<bool> SideExists(string side);
    public Task InsertSide(Side side);
    public Task<Image?> GetByCardIdAndSide(
        CardId cardId,
        string side);
    public void DeleteImage(Image image);
    public Task SaveChangesAsync();
}