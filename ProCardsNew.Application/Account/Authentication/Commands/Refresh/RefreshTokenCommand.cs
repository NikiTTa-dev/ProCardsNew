using ErrorOr;
using MediatR;
using ProCardsNew.Application.Account.Authentication.Common;

namespace ProCardsNew.Application.Account.Authentication.Commands.Refresh;

public record RefreshTokenCommand(
    Guid UserId,
    string? RefreshToken) 
    : IRequest<ErrorOr<AuthenticationResult>>;