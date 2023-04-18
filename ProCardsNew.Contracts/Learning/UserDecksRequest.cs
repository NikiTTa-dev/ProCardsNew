namespace ProCardsNew.Contracts.Learning;

public record UserDecksRequest(
    Guid UserId,
    string SearchQuery = "");