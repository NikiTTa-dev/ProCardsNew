using ErrorOr;
using MediatR;

namespace ProCardsNew.Application.Editing.Decks.Commands.AddCard;

public record AddCardCommand(
    Guid UserId,
    Guid DeckId,
    Guid CardId)
    : IRequest<ErrorOr<AddCardCommandResult>>;