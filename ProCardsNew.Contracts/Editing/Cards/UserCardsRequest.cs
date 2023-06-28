namespace ProCardsNew.Contracts.Editing.Cards;

public record UserCardsRequest(
    Guid UserId,
    string SearchQuery = "");