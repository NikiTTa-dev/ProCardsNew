namespace ProCardsNew.Contracts.Editing.Decks;

public record RemoveCardFromDeckRequest(
    Guid UserId,
    Guid DeckId,
    Guid CardId);