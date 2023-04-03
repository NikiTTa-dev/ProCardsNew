namespace ProCardsNew.Application.Editing.Decks.Commands.CreateDeck;

public record CreateDeckCommandResult(
    Guid DeckId,
    string Result = "Success");