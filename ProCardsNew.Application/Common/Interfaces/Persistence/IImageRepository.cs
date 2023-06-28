using ProCardsNew.Domain.CardAggregate.Entities;
using ProCardsNew.Domain.CardAggregate.ValueObjects;

namespace ProCardsNew.Application.Common.Interfaces.Persistence;

public interface IImageRepository
{
    public Task<Image?> GetFrontImageByCardId(
        CardId cardId);
    public Task<Image?> GetBackImageByCardId(
        CardId cardId);
    public void DeleteImage(Image image);
    public Task SaveChangesAsync();
}