namespace ProCardsNew.Contracts.Common;

public record DeckPreviewResponse(
    Guid DeckId,
    string Name,
    string Description,
    bool IsPrivate);