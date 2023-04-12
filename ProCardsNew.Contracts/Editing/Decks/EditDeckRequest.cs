namespace ProCardsNew.Contracts.Editing.Decks;

public record EditDeckRequest(
    Guid DeckId,
    Guid UserId,
    string Name,
    string Description);