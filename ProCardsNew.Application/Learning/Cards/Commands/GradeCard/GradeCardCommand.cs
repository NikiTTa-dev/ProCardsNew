using ErrorOr;
using MediatR;

namespace ProCardsNew.Application.Learning.Cards.Commands.GradeCard;

public record GradeCardCommand(
    Guid UserId,
    Guid DeckId,
    Guid CardId,
    int Grade,
    float TimeInSeconds)
    : IRequest<ErrorOr<GradeCardCommandResult>>;