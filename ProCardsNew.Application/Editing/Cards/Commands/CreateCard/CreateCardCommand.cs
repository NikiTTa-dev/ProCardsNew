using ErrorOr;
using MediatR;

namespace ProCardsNew.Application.Editing.Cards.Commands.CreateCard;

public record CreateCardCommand(
    Guid UserId,
    Guid DeckId,
    string FrontSide,
    string BackSide)
    : IRequest<ErrorOr<CreateCardCommandResult>>;