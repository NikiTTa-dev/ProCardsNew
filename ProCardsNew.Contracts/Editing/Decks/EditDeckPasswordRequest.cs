namespace ProCardsNew.Contracts.Editing.Decks;

public record EditDeckPasswordRequest(
    Guid DeckId,
    Guid UserId,
    bool IsPrivate,
    string Password);