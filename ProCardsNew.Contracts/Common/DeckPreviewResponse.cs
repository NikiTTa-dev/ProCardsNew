namespace ProCardsNew.Contracts.Common;

public record DeckPreviewResponse(
    Guid DeckId,
    string Name,
    bool IsOwner);