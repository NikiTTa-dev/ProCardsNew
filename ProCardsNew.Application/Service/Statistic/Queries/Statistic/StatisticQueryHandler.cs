using ErrorOr;
using MediatR;
using ProCardsNew.Application.Common.Interfaces.Persistence;

namespace ProCardsNew.Application.Service.Statistic.Queries.Statistic;

public class StatisticQueryHandler
    : IRequestHandler<StatisticQuery, ErrorOr<StatisticQueryResult>>
{
    private readonly IStatisticRepository _statisticRepository;

    public StatisticQueryHandler(
        IStatisticRepository statisticRepository)
    {
        _statisticRepository = statisticRepository;
    }
    
    public async Task<ErrorOr<StatisticQueryResult>> Handle(StatisticQuery query, CancellationToken cancellationToken)
    {
        var statistic = await _statisticRepository.GetTopStatisticIncludeUserAsync();
        return new StatisticQueryResult(statistic.ConvertAll(s =>
            new StatisticPreview(
                UserId: s.Id.Value,
                Login: s.User?.Login ?? "NotFound",
                Score: s.Score)));
    }
}