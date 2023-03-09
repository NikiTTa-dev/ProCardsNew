using ErrorOr;
using MediatR;
using ProCardsNew.Application.Account.Authentication.Common;

namespace ProCardsNew.Application.Account.Authentication.Queries.Login;

public record LoginQuery(
    string Login,
    string Password) 
    : IRequest<ErrorOr<AuthenticationResult>>;