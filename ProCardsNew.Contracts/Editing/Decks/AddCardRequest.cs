namespace ProCardsNew.Contracts.Editing.Decks;

public record AddCardRequest(
    Guid UserId,
    Guid DeckId,
    Guid CardId);