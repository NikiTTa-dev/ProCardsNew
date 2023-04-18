namespace ProCardsNew.Application.Learning.Decks.Common;

public record DeckResult(
    Guid Id,
    string Name,
    string Description,
    Guid OwnerId,
    string OwnerLogin,
    int CardsCount,
    List<DeckStatisticResult> Statistics);
    
public record DeckStatisticResult(
    Guid UserId,
    string Login,
    int Score);