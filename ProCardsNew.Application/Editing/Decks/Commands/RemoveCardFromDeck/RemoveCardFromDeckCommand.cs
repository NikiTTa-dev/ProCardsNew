using ErrorOr;
using MediatR;

namespace ProCardsNew.Application.Editing.Decks.Commands.RemoveCardFromDeck;

public record RemoveCardFromDeckCommand(
    Guid UserId,
    Guid DeckId,
    Guid CardId)
    : IRequest<ErrorOr<RemoveCardFromDeckCommandResult>>;