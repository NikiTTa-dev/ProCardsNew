﻿namespace ProCardsNew.Application.Editing.Decks.Queries.UserDecksToEdit;

public record UserDecksToEditQueryResult(
    List<DeckPreview> DeckPreviews);

public record DeckPreview(
    Guid DeckId,
    string DeckName,
    string Description,
    bool IsPrivate);