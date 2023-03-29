using ErrorOr;
using MediatR;
using ProCardsNew.Application.Account.PasswordRecovery.Common;

namespace ProCardsNew.Application.Account.PasswordRecovery.Commands.PasswordRecoveryNewPassword;

public record PasswordRecoveryNewPasswordCommand(
    string Code,
    string Email,
    string Password) : IRequest<ErrorOr<PasswordRecoveryResult>>;