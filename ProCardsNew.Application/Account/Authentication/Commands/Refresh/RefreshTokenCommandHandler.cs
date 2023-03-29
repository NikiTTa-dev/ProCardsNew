using ErrorOr;
using MediatR;
using ProCardsNew.Application.Account.Authentication.Common;
using ProCardsNew.Application.Common.Interfaces.Authentication;
using ProCardsNew.Application.Common.Interfaces.Persistence;
using ProCardsNew.Domain.Common.Errors;
using ProCardsNew.Domain.UserAggregate.ValueObjects;

namespace ProCardsNew.Application.Account.Authentication.Commands.Refresh;

public class RefreshTokenCommandHandler 
    : IRequestHandler<RefreshTokenCommand, ErrorOr<AuthenticationResult>>
{
    private readonly IUserRepository _userRepository;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;

    public RefreshTokenCommandHandler(IUserRepository userRepository, IJwtTokenGenerator jwtTokenGenerator)
    {
        _userRepository = userRepository;
        _jwtTokenGenerator = jwtTokenGenerator;
    }

    public async Task<ErrorOr<AuthenticationResult>> Handle(RefreshTokenCommand command, CancellationToken cancellationToken)
    {
        if (await _userRepository.GetUserByIdAsync(UserId.Create(command.UserId)) is not { } user)
            return Errors.User.NotFound;

        if (user.RefreshToken!.Value != command.RefreshToken)
            return Errors.Authentication.InvalidRefreshToken;

        var access = _jwtTokenGenerator.GenerateToken(user);
        var refresh = user.GenerateRefreshToken();
        
        await _userRepository.SaveChangesAsync();

        return new AuthenticationResult(user, access, refresh.Value);
    }
}