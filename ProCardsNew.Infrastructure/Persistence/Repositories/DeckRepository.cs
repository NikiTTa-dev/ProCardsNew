using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using ProCardsNew.Application.Common.Interfaces.Persistence;
using ProCardsNew.Domain.DeckAggregate;
using ProCardsNew.Domain.DeckAggregate.ValueObjects;
using ProCardsNew.Domain.UserAggregate.ValueObjects;

namespace ProCardsNew.Infrastructure.Persistence.Repositories;

public class DeckRepository: IDeckRepository
{
    private readonly ProCardsDbContext _dbContext;

    public DeckRepository(ProCardsDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddAsync(Deck deck)
    {
        await _dbContext.Decks.AddAsync(deck);
    }

    public async Task<Deck?> GetByIdAsync(DeckId id)
    {
        return await _dbContext.Decks
            .FirstOrDefaultAsync(u => u.Id == id);
    }

    public async Task<Deck?> GetByNameAsync(UserId userId, string name)
    {
        return await _dbContext.Decks
            .Where(d => d.Name == name)
            .FirstOrDefaultAsync(d => d.OwnerId == userId);
    }
    
    public async Task<List<Deck>> GetByOwnerIdWhereAsync(
        UserId userId,
        Expression<Func<Deck, bool>> filter,
        Expression<Func<Deck, object>> orderByDesc)
    {
        return await _dbContext.Decks
            .Where(filter)
            .Where(d => d.OwnerId == userId)
            .OrderByDescending(orderByDesc)
            .ToListAsync();
    }

    public async Task<List<Deck>> GetAllIncludingAsync(params Expression<Func<Deck, object?>>[] includeProperties)
    {
        var decks = _dbContext.Decks.AsQueryable();
        
        foreach (var includedProperty in includeProperties)
        {
            decks = decks.Include(includedProperty);
        }

        return await decks.ToListAsync();
    }

    public async Task<List<Deck>> GetWhereIncludingAsync(
        Expression<Func<Deck, bool>> filter,
        params Expression<Func<Deck, object?>>[] includeProperties)
    {
        var decks = _dbContext.Decks
            .Where(filter);
        
        foreach (var includedProperty in includeProperties)
        {
            decks = decks.Include(includedProperty);
        }

        return await decks.ToListAsync();
    }

    public void DeleteAsync(Deck deck)
    {
        _dbContext.Remove(deck);
    }

    public async Task SaveChangesAsync()
    {
        await _dbContext.SaveChangesAsync();
    }
}