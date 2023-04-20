using ErrorOr;
using MediatR;

namespace ProCardsNew.Application.Learning.Cards.Queries.StudyCards;

public record StudyCardsQuery(
    Guid UserId,
    Guid DeckId)
    : IRequest<ErrorOr<StudyCardsQueryResult>>;