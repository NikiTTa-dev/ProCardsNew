namespace ProCardsNew.Contracts.Editing.Cards;

public record EditCardRequest(
    Guid UserId,
    Guid CardId,
    string FrontSide,
    string BackSide);