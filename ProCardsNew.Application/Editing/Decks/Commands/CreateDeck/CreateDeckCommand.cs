using ErrorOr;
using MediatR;

namespace ProCardsNew.Application.Editing.Decks.Commands.CreateDeck;

public record CreateDeckCommand(
    Guid UserId,
    string Name,
    string Description)
    : IRequest<ErrorOr<CreateDeckCommandResult>>;