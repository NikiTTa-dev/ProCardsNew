using ErrorOr;
using MediatR;
using ProCardsNew.Application.Account.PasswordRecovery.Common;

namespace ProCardsNew.Application.Account.PasswordRecovery.Queries.PasswordRecoveryCode;

public record PasswordRecoveryCodeQuery(
    string Email,
    string Code) : IRequest<ErrorOr<PasswordRecoveryResult>>;