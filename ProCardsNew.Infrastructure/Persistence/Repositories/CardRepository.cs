using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using ProCardsNew.Application.Common.Interfaces.Persistence;
using ProCardsNew.Domain.CardAggregate;
using ProCardsNew.Domain.CardAggregate.ValueObjects;
using ProCardsNew.Domain.DeckAggregate.ValueObjects;
using ProCardsNew.Domain.UserAggregate.ValueObjects;

namespace ProCardsNew.Infrastructure.Persistence.Repositories;

public class CardRepository : RepositoryBase, ICardRepository
{
    public CardRepository(ProCardsDbContext dbContext)
        : base(dbContext)
    {
    }

    public async Task AddAsync(Card card)
    {
        await DbContext.Cards.AddAsync(card);
    }

    public async Task<List<Card>> GetCardsWithGradesAsync(DeckId deckId, UserId userId)
    {
        return await DbContext.Cards
            .Include(c => c.FrontImage)
            .Include(c => c.BackImage)
            .Include(c => c.Grades
                .Where(g => g.UserId == userId)
                .Take(5))
            .Where(c => c.DeckCards.FirstOrDefault(dc => dc.DeckId == deckId) != null)
            .ToListAsync();
    }

    public async Task<Card?> GetByIdAsync(CardId id)
    {
        return await DbContext.Cards
            .FirstOrDefaultAsync(u => u.Id == id);
    }

    public async Task<Card?> GetByNameAsync(
        UserId userId,
        string frontSide,
        string backSide)
    {
        return await DbContext.Cards
            .Where(d => d.FrontSide == frontSide && d.BackSide == backSide)
            .FirstOrDefaultAsync(d => d.OwnerId == userId);
    }

    public async Task<List<Card>> GetByOwnerIdWhereAsync(
        UserId userId,
        Expression<Func<Card, bool>> filter,
        Expression<Func<Card, object>> orderByDesc)
    {
        return await DbContext.Cards
            .Include(c => c.FrontImage)
            .Include(c => c.BackImage)
            .Where(filter)
            .Where(c => c.OwnerId == userId)
            .OrderByDescending(orderByDesc)
            .ToListAsync();
    }

    public async Task<List<Card>> GetByOwnerIdAndDeckIdWhereAsync(
        UserId userId,
        DeckId deckId,
        Expression<Func<Card, bool>> filter,
        Expression<Func<Card, object>> orderByDesc)
    {
        return await DbContext.Cards
            .Include(c => c.FrontImage)
            .Include(c => c.BackImage)
            .Where(filter)
            .Where(c => c.OwnerId == userId)
            .Where(c => c.Decks.Count(d => d.Id == deckId) > 0)
            .OrderByDescending(orderByDesc)
            .ToListAsync();
    }

    public async Task<bool> HasFrontImageAsync(
        CardId cardId)
    {
        return await DbContext.Cards
            .Where(c => c.Id == cardId)
            .Where(c => c.FrontImage != null)
            .AnyAsync();
    }
    
    public async Task<bool> HasBackImageAsync(
        CardId cardId)
    {
        return await DbContext.Cards
            .Where(c => c.Id == cardId)
            .Where(c => c.BackImage != null)
            .AnyAsync();
    }

    public void Delete(Card card)
    {
        DbContext.Cards.Remove(card);
    }

    public async Task SaveChangesAsync()
    {
        await DbContext.SaveChangesAsync();
    }
}