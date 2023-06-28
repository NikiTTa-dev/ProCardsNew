using ProCardsNew.Application.Editing.Cards.Queries.DeckCards;

namespace ProCardsNew.Application.Editing.Cards.Queries.UserCards;

public record UserCardsQueryResult(
    List<CardResult> Cards);