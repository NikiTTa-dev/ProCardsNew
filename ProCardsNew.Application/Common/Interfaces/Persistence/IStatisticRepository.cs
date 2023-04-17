using ProCardsNew.Domain.UserAggregate.Entities;

namespace ProCardsNew.Application.Common.Interfaces.Persistence;

public interface IStatisticRepository
{
    public Task<List<Statistic>> GetTopStatisticIncludeUserAsync();
}