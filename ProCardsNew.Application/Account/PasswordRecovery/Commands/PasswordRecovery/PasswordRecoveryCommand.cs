using ErrorOr;
using MediatR;
using ProCardsNew.Application.Account.PasswordRecovery.Common;

namespace ProCardsNew.Application.Account.PasswordRecovery.Commands.PasswordRecovery;

public record PasswordRecoveryCommand(
    string Login) 
    : IRequest<ErrorOr<PasswordRecoveryResult>>;