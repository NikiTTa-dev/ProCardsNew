using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using ProCardsNew.Application.Common.Interfaces.Persistence;
using ProCardsNew.Domain.CardAggregate.ValueObjects;
using ProCardsNew.Domain.DeckAggregate;
using ProCardsNew.Domain.DeckAggregate.Entities;
using ProCardsNew.Domain.DeckAggregate.ValueObjects;
using ProCardsNew.Domain.UserAggregate.Entities;
using ProCardsNew.Domain.UserAggregate.ValueObjects;

namespace ProCardsNew.Infrastructure.Persistence.Repositories;

public class DeckRepository : IDeckRepository
{
    private readonly ProCardsDbContext _dbContext;

    public DeckRepository(ProCardsDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void ChangeStateToAdded(object entity)
    {
        _dbContext.Entry(entity).State = EntityState.Added;
    }

    public async Task AddAsync(Deck deck)
    {
        await _dbContext.Decks.AddAsync(deck);
    }
    
    public async Task<List<Deck>> GetUserDecks(UserId userId, string searchQuery)
    {
        return await _dbContext.UserDecks
            .OrderByDescending(ud => ud.LastOpenedAtDateTime)
            .Include(ud => ud.DeckAccess!.Deck!.Owner)
            .Where(ud => ud.UserId == userId)
            .Select(ud => ud.DeckAccess!)
            .Where(da => da.IsAccessible || da.Deck!.OwnerId == userId)
            .Select(da => da.Deck!)
            .Where(d => d.Name.ToUpper().Contains(searchQuery.ToUpper()))
            .ToListAsync();
    }

    public async Task<DeckAccess?> GetAccessibleDeckAccessAsync(
        DeckId deckId)
    {
        return await _dbContext.DeckAccesses
            .FirstOrDefaultAsync(da =>
                da.DeckId == deckId && da.IsAccessible == true);
    }

    public async Task<UserDeck?> GetUserDeck(
        DeckId deckId,
        UserId userId)
    {
        return await _dbContext.DeckAccesses
            .Where(da => da.DeckId == deckId && 
                         (da.IsAccessible || da.Deck!.OwnerId == userId))
            .SelectMany(da => da.UserDecks)
            .Where(ud => ud.UserId == userId)
            .FirstOrDefaultAsync();
    }

    public async Task<Deck?> GetByIdAsync(DeckId id)
    {
        return await _dbContext.Decks.FirstOrDefaultAsync(d => d.Id == id);
    }

    public async Task<Deck?> GetByIdIncludingAsync(
        DeckId id,
        params Expression<Func<Deck, object>>[] includedProperties)
    {
        var decks = _dbContext.Decks
            .Where(d => d.Id == id);

        foreach (var includedProperty in includedProperties)
        {
            decks = decks.Include(includedProperty);
        }

        return await decks.FirstOrDefaultAsync();
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

    public async Task<int> GetCardsCount(DeckId deckId)
    {
        return await _dbContext.DeckCards
            .Where(d => d.DeckId == deckId)
            .CountAsync();
    }

    public async Task<bool> HasAccess(DeckId deckId, UserId userId)
    {
        var a = await _dbContext.DeckAccesses
            .Where(da => da.DeckId == deckId &&
                         (da.Deck!.OwnerId == userId ||
                          (da.IsAccessible &&
                           da.UserDecks
                               .FirstOrDefault(ud => ud.UserId == userId)
                           != null)))
            .FirstOrDefaultAsync();
        return a != null;
    }

    public async Task<bool> HasCard(DeckId deckId, CardId cardId)
    {
        return await _dbContext.DeckCards
            .Where(dc => dc.DeckId == deckId && dc.CardId == cardId)
            .FirstOrDefaultAsync() != null;
    }

    public async Task RemoveCardFromDeck(DeckId deckId, CardId cardId)
    {
        var deckCard = await _dbContext.DeckCards
            .FirstOrDefaultAsync(dc => dc.DeckId == deckId && dc.CardId == cardId);

        if(deckCard is not null)
            _dbContext.Remove(deckCard);
    }

    public void DeleteUserDeck(UserDeck userDeck)
    {
        _dbContext.Remove(userDeck);
    }

    public void Delete(Deck deck)
    {
        _dbContext.Remove(deck);
    }

    public async Task SaveChangesAsync()
    {
        await _dbContext.SaveChangesAsync();
    }
}