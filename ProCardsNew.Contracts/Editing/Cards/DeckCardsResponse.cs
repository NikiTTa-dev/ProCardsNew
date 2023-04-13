using ProCardsNew.Contracts.Common;

namespace ProCardsNew.Contracts.Editing.Cards;

public record DeckCardsResponse(
    List<CardResponse> Cards);