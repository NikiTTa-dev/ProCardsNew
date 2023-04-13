namespace ProCardsNew.Application.Editing.Cards.Queries.DeckCards;

public record DeckCardsQueryResult(
    List<CardResult> Cards);
    
public record CardResult(
    Guid Id,
    string FrontSide,
    string BackSide,
    bool HasFrontImage,
    bool HasBackImage);