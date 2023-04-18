namespace ProCardsNew.Contracts.Learning;

public record AddDeckRequest(
    Guid UserId,
    Guid DeckId,
    string Password);