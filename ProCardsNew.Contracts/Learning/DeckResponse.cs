namespace ProCardsNew.Contracts.Learning;

public record DeckResponse(
    Guid Id,
    string DeckName,
    string Description,
    Guid OwnerId,
    string OwnerLogin,
    int CardsCount,
    List<DeckStatisticResponse> Statistics);
    
public record DeckStatisticResponse(
    Guid UserId,
    string Login,
    int Score);