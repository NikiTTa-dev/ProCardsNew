namespace ProCardsNew.Contracts.Common;

public record DeckPreviewResponse(
    Guid DeckId,
    string DeckName,
    string Description,
    bool IsPrivate);