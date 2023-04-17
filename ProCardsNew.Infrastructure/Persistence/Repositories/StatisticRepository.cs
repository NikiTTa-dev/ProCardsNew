using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using ProCardsNew.Application.Common.Interfaces.Persistence;
using ProCardsNew.Application.Common.Settings;
using ProCardsNew.Domain.UserAggregate.Entities;

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
}