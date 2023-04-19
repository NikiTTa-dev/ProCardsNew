namespace ProCardsNew.Application.Learning.Decks.Queries.UserDecks;

public record UserDecksQueryResult(
    List<UserDeckPreview> Decks);
    
public record UserDeckPreview(
    Guid DeckId,
    string DeckName,
    bool IsOwner);