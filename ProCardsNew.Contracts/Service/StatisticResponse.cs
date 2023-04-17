namespace ProCardsNew.Contracts.Service;

public record StatisticResponse(
    List<StatisticPreviewResponse> Statistics);

public record StatisticPreviewResponse(
    Guid UserId,
    string Login,
    int Score);