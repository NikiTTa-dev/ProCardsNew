namespace ProCardsNew.Application.Service.Statistic.Queries.Statistic;

public record StatisticQueryResult(
    List<StatisticPreview> Statistics);

public record StatisticPreview(
    Guid UserId,
    string Login,
    int Score);