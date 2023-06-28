using Microsoft.EntityFrameworkCore;
using ProCardsNew.Application.Common.Interfaces.Persistence;
using ProCardsNew.Domain.CardAggregate.Entities;
using ProCardsNew.Domain.CardAggregate.ValueObjects;

namespace ProCardsNew.Infrastructure.Persistence.Repositories;

public class ImageRepository : RepositoryBase, IImageRepository
{
    public ImageRepository(ProCardsDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<Image?> GetFrontImageByCardId(
        CardId cardId)
    {
        return await DbContext.Cards
            .Where(c => c.Id == cardId)
            .Include(c => c.FrontImage!.ImageData)
            .Select(c => c.FrontImage)
            .FirstOrDefaultAsync();
    }
    
    public async Task<Image?> GetBackImageByCardId(
        CardId cardId)
    {
        return await DbContext.Cards
            .Where(c => c.Id == cardId)
            .Include(c => c.BackImage!.ImageData)
            .Select(c => c.BackImage)
            .FirstOrDefaultAsync();
    }

    public void DeleteImage(Image image)
    {
        DbContext.Images.Remove(image);
    }

    public async Task SaveChangesAsync()
    {
        await DbContext.SaveChangesAsync();
    }
}