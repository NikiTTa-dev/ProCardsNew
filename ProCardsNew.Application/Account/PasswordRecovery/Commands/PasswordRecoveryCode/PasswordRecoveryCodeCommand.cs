using ErrorOr;
using MediatR;
using ProCardsNew.Application.Account.PasswordRecovery.Common;

namespace ProCardsNew.Application.Account.PasswordRecovery.Commands.PasswordRecoveryCode;

public record PasswordRecoveryCodeCommand(
    string Email,
    string Code) : IRequest<ErrorOr<PasswordRecoveryResult>>;