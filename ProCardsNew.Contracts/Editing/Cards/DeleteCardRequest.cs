namespace ProCardsNew.Contracts.Editing.Cards;

public record DeleteCardRequest(
    Guid UserId,
    Guid CardId);