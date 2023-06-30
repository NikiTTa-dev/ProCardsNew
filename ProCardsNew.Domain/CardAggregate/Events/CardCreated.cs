using ProCardsNew.Domain.Common.Models;
using ProCardsNew.Domain.UserAggregate.ValueObjects;

namespace ProCardsNew.Domain.CardAggregate.Events;

public record CardCreated(
    Card Card,
    UserId UserId)
    : IDomainEvent;