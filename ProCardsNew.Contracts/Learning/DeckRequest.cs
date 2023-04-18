namespace ProCardsNew.Contracts.Learning;

public record DeckRequest(
    Guid UserId,
    Guid DeckId);