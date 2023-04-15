namespace ProCardsNew.Contracts.Editing.Cards;

public record CreateCardRequest(
    Guid UserId,
    Guid DeckId,
    string FrontSide,
    string BackSide);