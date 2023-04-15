namespace ProCardsNew.Contracts.Service;

public record AddCardImageRequest(
    Guid UserId,
    Guid CardId,
    string Side);