using ErrorOr;
using MediatR;

namespace ProCardsNew.Application.Editing.Decks.Commands.EditDeck;

public record EditDeckCommand(
    Guid DeckId,
    Guid UserId,
    string Name,
    string Description)
    : IRequest<ErrorOr<EditDeckResult>>;