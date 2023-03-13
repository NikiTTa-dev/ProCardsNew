using ProCardsNew.Domain.UserAggregate;

namespace ProCardsNew.Application.Account.Authentication.Common;

public record AuthenticationResult(
    User User,
    string Token,
    string RefreshToken);