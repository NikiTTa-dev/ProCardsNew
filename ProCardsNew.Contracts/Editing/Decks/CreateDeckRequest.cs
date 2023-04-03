namespace ProCardsNew.Contracts.Editing.Decks;

public record CreateDeckRequest(
    Guid UserId,
    string Name,
    string Description,
    bool IsPrivate,
    string Password);