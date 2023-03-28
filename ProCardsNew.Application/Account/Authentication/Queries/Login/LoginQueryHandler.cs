using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Identity;
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
    private readonly IPasswordHasherService _passwordHasherService;

    public LoginQueryHandler(
        IJwtTokenGenerator jwtTokenGenerator,
        IUserRepository userRepository,
        IPasswordHasherService passwordHasherService)
    {
        _jwtTokenGenerator = jwtTokenGenerator;
        _userRepository = userRepository;
        _passwordHasherService = passwordHasherService;
    }

    public async Task<ErrorOr<AuthenticationResult>> Handle(LoginQuery query, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

        if (_userRepository.GetUserByLogin(query.Login.ToUpper()) is not { } user)
            return Errors.Authentication.InvalidCredentials;

        var passwordVerificationResult = _passwordHasherService.VerifyPasswordHash(
            user.PasswordHash,
            query.Password);

        if (passwordVerificationResult == PasswordVerificationResult.Failed)
            return Errors.Authentication.InvalidCredentials;

        if (passwordVerificationResult == PasswordVerificationResult.SuccessRehashNeeded)
            user.RehashPassword(_passwordHasherService.GeneratePasswordHash(query.Password));

        var token = _jwtTokenGenerator.GenerateToken(user);
        var refresh = user.GenerateRefreshToken();
        
        _userRepository.SaveChanges();
        
        return new AuthenticationResult(
            user,
            token,
            refresh.Value);
    }
}