using System.Linq.Expressions;
using ProCardsNew.Domain.DeckAggregate;
using ProCardsNew.Domain.DeckAggregate.ValueObjects;
using ProCardsNew.Domain.UserAggregate.ValueObjects;

namespace ProCardsNew.Application.Common.Interfaces.Persistence;

public interface IDeckRepository
{
    Task AddAsync(Deck deck);
    
    Task<Deck?> GetByIdAsync(DeckId id);
    
    Task<Deck?> GetByNameAsync(UserId userId, string name);
    
    Task<List<Deck>> GetByOwnerIdWhereAsync(
        UserId userId,
        Expression<Func<Deck, bool>> filter,
        Expression<Func<Deck, object>> orderByDesc);
    
    Task<List<Deck>> GetAllIncludingAsync(params Expression<Func<Deck, object?>>[] includeProperties);
    
    Task<List<Deck>> GetWhereIncludingAsync(
        Expression<Func<Deck, bool>> filter,
        params Expression<Func<Deck, object?>>[] includeProperties);

    void DeleteAsync(Deck deck);
    
    Task SaveChangesAsync();
}