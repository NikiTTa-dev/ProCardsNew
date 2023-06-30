using ProCardsNew.Domain.Common.Models;
using ProCardsNew.Domain.DeckAggregate.ValueObjects;
using ProCardsNew.Domain.UserAggregate.ValueObjects;

namespace ProCardsNew.Domain.CardAggregate.Events;

public record CardGraded(
        Card Card,
        DeckId DeckId,
        UserId UserId,
        float Hours)
    : IDomainEvent;