using ErrorOr;
using MediatR;
using ProCardsNew.Application.Account.Authentication.Common;
using ProCardsNew.Application.Common.Interfaces.Authentication;
using ProCardsNew.Application.Common.Interfaces.Persistence;
using ProCardsNew.Domain.Common.Errors;

namespace ProCardsNew.Application.Account.Authentication.Queries.Login;

public class LoginQueryHandler :
    IRequestHandler<LoginQuery, ErrorOr<AuthenticationResult>>
{
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IUserRepository _userRepository;

    public LoginQueryHandler(IJwtTokenGenerator jwtTokenGenerator, IUserRepository userRepository)
    {
        _jwtTokenGenerator = jwtTokenGenerator;
        _userRepository = userRepository;
    }

    public async Task<ErrorOr<AuthenticationResult>> Handle(LoginQuery query, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

        if (_userRepository.GetUserByLogin(query.Login.ToUpper()) is not { } user)
            return Errors.Authentication.InvalidCredentials;

        if (user.PasswordHash != query.Password)
            return Errors.Authentication.InvalidCredentials;

        var token = _jwtTokenGenerator.GenerateToken(user);
        var refresh = user.GenerateRefreshToken();
        
        return new AuthenticationResult(
            user,
            token,
            refresh.Value);
    }
}