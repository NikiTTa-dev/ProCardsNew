using ErrorOr;
using MediatR;
using ProCardsNew.Application.Account.Authentication.Common;

namespace ProCardsNew.Application.Account.Authentication.Commands.Register;

public record RegisterCommand(
    string Login,
    string FirstName,
    string LastName,
    string Location,
    string Email,
    string Password) 
    : IRequest<ErrorOr<AuthenticationResult>>;