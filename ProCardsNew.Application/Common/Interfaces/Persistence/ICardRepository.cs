﻿using System.Linq.Expressions;
using ProCardsNew.Domain.CardAggregate;
using ProCardsNew.Domain.CardAggregate.ValueObjects;
using ProCardsNew.Domain.DeckAggregate.ValueObjects;
using ProCardsNew.Domain.UserAggregate.ValueObjects;

namespace ProCardsNew.Application.Common.Interfaces.Persistence;

public interface ICardRepository
{
    public Task AddAsync(Card card);

    public Task<Card?> GetByIdAsync(CardId id);

    public Task<Card?> GetByNameAsync(UserId userId, string frontSide);

    public Task<List<Card>> GetByOwnerIdWhereAsync(
        UserId userId,
        Expression<Func<Card, bool>> filter,
        Expression<Func<Card, object>> orderByDesc);

    public Task<List<Card>> GetByOwnerIdAndDeckIdWhereAsync(
        UserId userId,
        DeckId deckId,
        Expression<Func<Card, bool>> filter,
        Expression<Func<Card, object>> orderByDesc);

    public Task<bool> HasImage(
        CardId cardId,
        string side);

    public void DeleteAsync(Card card);

    public Task SaveChangesAsync();
}