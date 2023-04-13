namespace ProCardsNew.Contracts.Editing.Decks;

public record DeleteDeckRequest(
    Guid UserId,
    Guid DeckId);