using ErrorOr;
using MediatR;

namespace ProCardsNew.Application.Editing.Cards.Commands.DeleteCard;

public record DeleteCardCommand(
    Guid UserId,
    Guid CardId)
    : IRequest<ErrorOr<DeleteCardCommandResult>>;