using ErrorOr;
using MediatR;
using ProCardsNew.Application.Account.Authentication.Common;

namespace ProCardsNew.Application.Account.Authentication.Commands.Login;

public record LoginCommand(
    string Login,
    string Password) 
    : IRequest<ErrorOr<AuthenticationResult>>;