using Mapster;
using ProCardsNew.Application.Service.Statistic.Queries.Statistic;
using ProCardsNew.Contracts.Service;

namespace ProCardsNew.Api.Common.Mapping.Service;

public class StatisticMapping : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<StatisticQueryResult, StatisticResponse>();
        config.NewConfig<StatisticPreview, StatisticPreviewResponse>();
    }
}