using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using ProCardsNew.Application.Common.Interfaces.Persistence;
using ProCardsNew.Domain.CardAggregate;
using ProCardsNew.Domain.CardAggregate.ValueObjects;
using ProCardsNew.Domain.DeckAggregate.ValueObjects;
using ProCardsNew.Domain.UserAggregate.ValueObjects;

namespace ProCardsNew.Infrastructure.Persistence.Repositories;

public class CardRepository : ICardRepository
{
    private readonly ProCardsDbContext _dbContext;

    public CardRepository(ProCardsDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddAsync(Card card)
    {
        await _dbContext.Cards.AddAsync(card);
    }

    public async Task<Card?> GetByIdAsync(CardId id)
    {
        return await _dbContext.Cards
            .FirstOrDefaultAsync(u => u.Id == id);
    }

    public async Task<Card?> GetByNameAsync(UserId userId, string frontSide)
    {
        return await _dbContext.Cards
            .Where(d => d.FrontSide == frontSide)
            .FirstOrDefaultAsync(d => d.OwnerId == userId);
    }
    
    public async Task<List<Card>> GetByOwnerIdWhereAsync(
        UserId userId,
        Expression<Func<Card, bool>> filter,
        Expression<Func<Card, object>> orderByDesc)
    {
        return await _dbContext.Cards
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
        return await _dbContext.Cards
            .Where(filter)
            .Where(c => c.OwnerId == userId)
            .Where(c => c.Decks.Count(d => d.Id == deckId) > 0)
            .OrderByDescending(orderByDesc)
            .ToListAsync();
    }

    public async Task<bool> HasImage(
        CardId cardId,
        string side)
    {
        return await _dbContext.Images
            .Include(i => i.Side)
            .CountAsync(i => i.Side!.SideName == side && i.CardId == cardId) > 0;
    }
    
    public void DeleteAsync(Card card)
    {
        _dbContext.Remove(card);
    }

    public async Task SaveChangesAsync()
    {
        await _dbContext.SaveChangesAsync();
    }
}