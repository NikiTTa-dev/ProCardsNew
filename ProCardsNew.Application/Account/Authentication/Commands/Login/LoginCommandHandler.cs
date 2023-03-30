using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using ProCardsNew.Application.Account.Authentication.Common;
using ProCardsNew.Application.Common.Interfaces.Authentication;
using ProCardsNew.Application.Common.Interfaces.Persistence;
using ProCardsNew.Application.Common.Settings;
using ProCardsNew.Domain.Common.Errors;
using ProCardsNew.Domain.UserAggregate.Enums;

namespace ProCardsNew.Application.Account.Authentication.Commands.Login;

public class LoginCommandHandler :
    IRequestHandler<LoginCommand, ErrorOr<AuthenticationResult>>
{
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasherService _passwordHasherService;
    private readonly LockoutSettings _lockoutSettings;

    public LoginCommandHandler(
        IJwtTokenGenerator jwtTokenGenerator,
        IUserRepository userRepository,
        IPasswordHasherService passwordHasherService,
        IOptions<LockoutSettings> lockoutSettings)
    {
        _jwtTokenGenerator = jwtTokenGenerator;
        _userRepository = userRepository;
        _passwordHasherService = passwordHasherService;
        _lockoutSettings = lockoutSettings.Value;
    }

    public async Task<ErrorOr<AuthenticationResult>> Handle(LoginCommand command, CancellationToken cancellationToken)
    {
        if (await _userRepository.GetUserByLoginAsync(command.Login.ToUpper()) is not { } user)
            return Errors.Authentication.InvalidCredentials;

        if (user.IsLockedOut())
            return Errors.Authentication.LockedOut;

        var passwordVerificationResult = _passwordHasherService.VerifyPasswordHash(
            user.PasswordHash,
            command.Password);

        if (passwordVerificationResult == PasswordVerificationResult.Failed)
        {
            var result = user.AccessFail(
                lockoutMinutes: _lockoutSettings.LockoutMinutes,
                accessFailedMaxCountInclusive: _lockoutSettings.AccessFailedMaxCountInclusive);
            if (result == AccessFailResult.AccessFailedCounterIncreased)
            {
                await _userRepository.SaveChangesAsync();
                return Errors.Authentication.InvalidCredentials;
            }

            await _userRepository.SaveChangesAsync();
            return Errors.Authentication.LockedOut;
        }

        if (passwordVerificationResult == PasswordVerificationResult.SuccessRehashNeeded)
            user.RehashPassword(_passwordHasherService.GeneratePasswordHash(command.Password));
        
        user.ResetAccessFailsCount();
        
        var token = _jwtTokenGenerator.GenerateToken(user);
        var refresh = user.GenerateRefreshToken();

        await _userRepository.SaveChangesAsync();

        return new AuthenticationResult(
            user,
            token,
            refresh.Value);
    }
}