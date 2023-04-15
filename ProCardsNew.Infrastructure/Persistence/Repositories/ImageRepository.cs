using Microsoft.EntityFrameworkCore;
using ProCardsNew.Application.Common.Interfaces.Persistence;
using ProCardsNew.Domain.CardAggregate.Entities;
using ProCardsNew.Domain.CardAggregate.ValueObjects;

namespace ProCardsNew.Infrastructure.Persistence.Repositories;

public class ImageRepository : IImageRepository
{
    private readonly ProCardsDbContext _dbContext;

    public ImageRepository(ProCardsDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Image?> GetByCardIdAndSide(
        CardId cardId,
        string side)
    {
        return await _dbContext.Images
            .FirstOrDefaultAsync(i =>
                i.CardId == cardId
                && i.Side!.SideName == side);
    }

    public void DeleteImage(Image image)
    {
        _dbContext.Images.Remove(image);
    }

    public async Task SaveChangesAsync()
    {
        await _dbContext.SaveChangesAsync();
    }
}