using ProCardsNew.Contracts.Common;

namespace ProCardsNew.Contracts.Editing.Cards;

public record UserCardsResponse(
    List<CardResponse> Cards);