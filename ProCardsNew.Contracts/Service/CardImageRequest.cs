namespace ProCardsNew.Contracts.Service;

public record CardImageRequest(
    Guid UserId,
    Guid DeckId,
    Guid CardId,
    string Side);