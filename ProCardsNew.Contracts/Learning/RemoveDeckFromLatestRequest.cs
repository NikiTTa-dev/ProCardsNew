namespace ProCardsNew.Contracts.Learning;

public record RemoveDeckFromLatestRequest(
    Guid UserId,
    Guid DeckId);