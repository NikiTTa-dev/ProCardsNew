namespace ProCardsNew.Application.Editing.Decks.Queries.UserDecksToEdit;

public record UserDecksToEditQueryResult(
    List<DeckPreview> DeckPreviews);

public record DeckPreview(
    Guid DeckId,
    string Name,
    string Description,
    bool IsPrivate);