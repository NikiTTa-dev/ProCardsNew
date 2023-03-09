using ProCardsNew.Domain.UserAggregate;

namespace ProCardsNew.Application.Common.Interfaces.Authentication;

public interface IJwtTokenGenerator
{
    string GenerateToken(User user);
}