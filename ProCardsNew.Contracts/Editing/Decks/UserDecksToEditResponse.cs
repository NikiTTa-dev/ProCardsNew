using ProCardsNew.Contracts.Common;

namespace ProCardsNew.Contracts.Editing.Decks;

public record UserDecksToEditResponse(
    List<DeckPreviewResponse> DeckPreviews);