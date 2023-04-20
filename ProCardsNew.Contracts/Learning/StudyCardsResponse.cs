using ProCardsNew.Contracts.Common;

namespace ProCardsNew.Contracts.Learning;

public record StudyCardsResponse(
    string DeckName,
    List<CardResponse> Cards);