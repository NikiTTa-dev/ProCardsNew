namespace ProCardsNew.Contracts.Service;

public record DeleteCardImageRequest(
    Guid UserId,
    Guid CardId,
    string Side);