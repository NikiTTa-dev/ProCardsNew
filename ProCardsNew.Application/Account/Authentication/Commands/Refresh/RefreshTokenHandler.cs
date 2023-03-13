using ErrorOr;
using MediatR;
using ProCardsNew.Application.Account.Authentication.Common;
using ProCardsNew.Application.Common.Interfaces.Authentication;
using ProCardsNew.Application.Common.Interfaces.Repositories;
using ProCardsNew.Domain.Common.Errors;

namespace ProCardsNew.Application.Account.Authentication.Commands.Refresh;

public class RefreshTokenHandler 
    : IRequestHandler<RefreshTokenCommand, ErrorOr<AuthenticationResult>>
{
    private readonly IUserRepository _userRepository;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;

    public RefreshTokenHandler(IUserRepository userRepository, IJwtTokenGenerator jwtTokenGenerator)
    {
        _userRepository = userRepository;
        _jwtTokenGenerator = jwtTokenGenerator;
    }

    public async Task<ErrorOr<AuthenticationResult>> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

        if (_userRepository.GetUserById(request.UserId) is not { } user)
            return Errors.User.NotFound;

        if (user.RefreshToken!.Value != request.RefreshToken)
            return Errors.Authentication.InvalidRefreshToken;

        var access = _jwtTokenGenerator.GenerateToken(user);
        var refresh = user.GenerateRefreshToken();
        
        return new AuthenticationResult(user, access, refresh.Value);
    }
}