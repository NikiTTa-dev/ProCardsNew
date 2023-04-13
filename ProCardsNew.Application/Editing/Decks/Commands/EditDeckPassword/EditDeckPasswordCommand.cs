using ErrorOr;
using MediatR;

namespace ProCardsNew.Application.Editing.Decks.Commands.EditDeckPassword;

public record EditDeckPasswordCommand(
    Guid DeckId,
    Guid UserId,
    bool IsPrivate,
    string Password)
    : IRequest<ErrorOr<EditDeckPasswordCommandResult>>;