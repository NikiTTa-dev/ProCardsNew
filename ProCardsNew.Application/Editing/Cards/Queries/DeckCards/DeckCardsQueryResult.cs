namespace ProCardsNew.Application.Editing.Cards.Queries.DeckCards;

public record DeckCardsQueryResult(
    string DeckName,
    List<CardResult> Cards);
    
public record CardResult(
    Guid Id,
    string FrontSide,
    string BackSide,
    bool HasFrontImage,
    bool HasBackImage);