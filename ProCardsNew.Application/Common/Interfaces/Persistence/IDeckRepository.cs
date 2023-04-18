using System.Linq.Expressions;
using ProCardsNew.Domain.CardAggregate.ValueObjects;
using ProCardsNew.Domain.DeckAggregate;
using ProCardsNew.Domain.DeckAggregate.Entities;
using ProCardsNew.Domain.DeckAggregate.ValueObjects;
using ProCardsNew.Domain.UserAggregate.Entities;
using ProCardsNew.Domain.UserAggregate.ValueObjects;

namespace ProCardsNew.Application.Common.Interfaces.Persistence;

public interface IDeckRepository
{
    void ChangeStateToAdd(object entity);
    Task AddAsync(Deck deck);

    public Task<List<Deck>> GetUserDecks(UserId userId, string searchQuery);
    
    Task<DeckAccess?> GetAccessibleDeckAccessAsync(
        DeckId deckId);

    public Task<UserDeck?> GetUserDeck(
        DeckId deckId,
        UserId userId);

    Task<Deck?> GetByIdAsync(DeckId id);

    Task<Deck?> GetByIdIncludingAsync(
        DeckId id,
        params Expression<Func<Deck, object>>[] includedProperties);

    Task<Deck?> GetByNameAsync(UserId userId, string name);

    Task<List<Deck>> GetByOwnerIdWhereAsync(
        UserId userId,
        Expression<Func<Deck, bool>> filter,
        Expression<Func<Deck, object>> orderByDesc);

    Task<List<Deck>> GetAllIncludingAsync(
        params Expression<Func<Deck, object?>>[] includeProperties);

    Task<List<Deck>> GetWhereIncludingAsync(
        Expression<Func<Deck, bool>> filter,
        params Expression<Func<Deck, object?>>[] includeProperties);

    public Task<int> GetCardsCount(DeckId deckId);
    public Task<bool> HasAccess(DeckId deckId, UserId userId);
    public Task<bool> HasCard(DeckId deckId, CardId cardId);
    
    void Delete(Deck deck);
    public void DeleteUserDeck(UserDeck userDeck);

    Task SaveChangesAsync();
}