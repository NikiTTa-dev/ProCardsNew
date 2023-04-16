using ProCardsNew.Contracts.Common;

namespace ProCardsNew.Contracts.Editing.Cards;

public record DeckCardsResponse(
    string DeckName,
    List<CardResponse> Cards);