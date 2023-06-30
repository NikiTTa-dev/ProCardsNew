using System.Linq.Expressions;
using ProCardsNew.Domain.DeckAggregate.Entities;
using ProCardsNew.Domain.DeckAggregate.ValueObjects;
using ProCardsNew.Domain.UserAggregate.Entities;
using ProCardsNew.Domain.UserAggregate.ValueObjects;

namespace ProCardsNew.Application.Common.Interfaces.Persistence;

public interface IStatisticRepository
{
    public Task<Statistic?> GetUserStatistic(UserId userId);
    public Task<DeckStatistic?> GetDeckStatisticsAsync(
        DeckId deckId,
        UserId userId);
    public Task<List<Statistic>> GetTopStatisticIncludeUserAsync();
    public Task<List<DeckStatistic>> GetDeckStatisticsIncludingAsync(
        DeckId deckId,
        params Expression<Func<DeckStatistic, object>>[] includedProperties);

    public Task<bool> HasStatistic(DeckId deckId, UserId userId);
}