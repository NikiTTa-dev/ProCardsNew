using ErrorOr;
using MediatR;
using ProCardsNew.Application.Account.PasswordRecovery.Common;

namespace ProCardsNew.Application.Account.PasswordRecovery.Commands.PasswordRecoveryNewPassword;

public record PasswordRecoveryNewPasswordCommand(
    string Code,
    string Login,
    string Password) : IRequest<ErrorOr<PasswordRecoveryResult>>;