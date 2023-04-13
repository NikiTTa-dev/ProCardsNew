using ErrorOr;
using MediatR;

namespace ProCardsNew.Application.Editing.Decks.Commands.DeleteDeck;

public record DeleteDeckCommand(
    Guid UserId,
    Guid DeckId)
    : IRequest<ErrorOr<DeleteDeckCommandResult>>;