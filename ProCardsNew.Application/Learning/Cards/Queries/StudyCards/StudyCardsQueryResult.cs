using ProCardsNew.Application.Editing.Cards.Queries.DeckCards;

namespace ProCardsNew.Application.Learning.Cards.Queries.StudyCards;

public record StudyCardsQueryResult(
    string DeckName,
    List<CardResult> Cards);