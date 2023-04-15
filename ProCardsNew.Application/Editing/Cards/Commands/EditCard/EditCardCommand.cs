using ErrorOr;
using MediatR;

namespace ProCardsNew.Application.Editing.Cards.Commands.EditCard;

public record EditCardCommand(
    Guid UserId,
    Guid CardId,
    string FrontSide,
    string BackSide)
    : IRequest<ErrorOr<EditCardCommandResult>>;