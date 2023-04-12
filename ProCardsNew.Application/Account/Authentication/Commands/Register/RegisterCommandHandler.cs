using ErrorOr;
using MediatR;
using ProCardsNew.Application.Account.Authentication.Common;
using ProCardsNew.Application.Common.Interfaces.Authentication;
using ProCardsNew.Application.Common.Interfaces.Persistence;
using ProCardsNew.Domain.Common.Errors;
using ProCardsNew.Domain.UserAggregate;

namespace ProCardsNew.Application.Account.Authentication.Commands.Register;

public class RegisterCommandHandler:
    IRequestHandler<RegisterCommand, ErrorOr<AuthenticationResult>>
{
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasherService _passwordHasherService;

    public RegisterCommandHandler(
        IJwtTokenGenerator jwtTokenGenerator,
        IUserRepository userRepository,
        IPasswordHasherService passwordHasherService)
    {
        _jwtTokenGenerator = jwtTokenGenerator;
        _userRepository = userRepository;
        _passwordHasherService = passwordHasherService;
    }

    public async Task<ErrorOr<AuthenticationResult>> Handle(RegisterCommand command, CancellationToken cancellationToken)
    {
        if (await _userRepository.GetByLoginAsync(command.Login.ToUpper()) is not null)
            return Errors.User.DuplicateLogin;

        var hashedPassword = _passwordHasherService.GeneratePasswordHash(command.Password);
        
        var user = User.Create(
            login: command.Login,
            email: command.Email,
            firstName: command.FirstName,
            lastName: command.LastName,
            location: command.Location,
            passwordHash: hashedPassword);
        
        await _userRepository.AddAsync(user);

        var token = _jwtTokenGenerator.GenerateToken(user);
        var refresh = user.GenerateRefreshToken();
        
        await _userRepository.SaveChangesAsync();
        
        return new AuthenticationResult(
            user,
            token,
            refresh.Value);
    }
}