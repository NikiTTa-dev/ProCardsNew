namespace ProCardsNew.Contracts.Learning;

public record GradeCardRequest(
    Guid UserId,
    Guid DeckId,
    Guid CardId,
    int Grade,
    float TimeInSeconds);