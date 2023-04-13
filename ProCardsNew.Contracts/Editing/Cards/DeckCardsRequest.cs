namespace ProCardsNew.Contracts.Editing.Cards;

public record DeckCardsRequest(
    Guid UserId,
    Guid DeckId,
    string SearchQuery);