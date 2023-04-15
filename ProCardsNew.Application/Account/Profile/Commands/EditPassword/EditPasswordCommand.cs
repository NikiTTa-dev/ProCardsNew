using ErrorOr;
using MediatR;

namespace ProCardsNew.Application.Account.Profile.Commands.EditPassword;

public record EditPasswordCommand(
    Guid UserId,
    string OldPassword,
    string NewPassword)
    : IRequest<ErrorOr<EditPasswordCommandResult>>;