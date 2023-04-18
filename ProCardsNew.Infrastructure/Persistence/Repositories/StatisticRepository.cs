using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using ProCardsNew.Application.Common.Interfaces.Persistence;
using ProCardsNew.Application.Common.Settings;
using ProCardsNew.Domain.DeckAggregate.Entities;
using ProCardsNew.Domain.DeckAggregate.ValueObjects;
using ProCardsNew.Domain.UserAggregate.Entities;
using ProCardsNew.Domain.UserAggregate.ValueObjects;

namespace ProCardsNew.Infrastructure.Persistence.Repositories;

public class StatisticRepository : IStatisticRepository
{
    private readonly ProCardsDbContext _dbContext;
    private readonly ValidationSettings _settings;

    public StatisticRepository(
        ProCardsDbContext dbContext,
        IOptions<ValidationSettings> settings)
    {
        _dbContext = dbContext;
        _settings = settings.Value;
    }

    public async Task<List<Statistic>> GetTopStatisticIncludeUserAsync()
    {
        return await _dbContext.Statistics
            .Include(s => s.User)
            .OrderByDescending(s => s.Score)
            .Take(_settings.StatisticTopUsersCount)
            .ToListAsync();
    }

    public async Task<List<DeckStatistic>> GetDeckStatisticsWhereIncludingAsync(
        DeckId deckId,
        params Expression<Func<DeckStatistic, object>>[] includedProperties)
    {
        var deckStatistics = _dbContext.DeckStatistics
            .Where(ds => ds.DeckId == deckId);

        foreach (var property in includedProperties)
        {
            deckStatistics = deckStatistics.Include(property);
        }

        return await deckStatistics.ToListAsync();
    }

    public async Task<bool> HasStatistic(DeckId deckId, UserId userId)
    {
        return await _dbContext.DeckStatistics
            .CountAsync(ds => ds.DeckId == deckId && ds.UserId == userId) > 0;
    }
}