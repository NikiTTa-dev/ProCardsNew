namespace ProCardsNew.Contracts.Learning;

public record StudyCardsRequest(
    Guid UserId,
    Guid DeckId);