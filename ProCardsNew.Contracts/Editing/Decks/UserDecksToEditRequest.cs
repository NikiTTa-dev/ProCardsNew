namespace ProCardsNew.Contracts.Editing.Decks;

public record UserDecksToEditRequest(
    Guid UserId,
    string SearchQuery);