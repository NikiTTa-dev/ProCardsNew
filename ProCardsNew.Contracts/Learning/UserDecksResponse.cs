namespace ProCardsNew.Contracts.Learning;

public record UserDecksResponse(
    List<UserDeckPreviewResponse> Decks);

public record UserDeckPreviewResponse(
    Guid DeckId,
    string DeckName,
    bool IsOwner);
    