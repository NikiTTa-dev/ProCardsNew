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
        await Task.CompletedTask;
        
        if (_userRepository.GetUserByLogin(command.Login.ToUpper()) is not null)
            return Errors.User.DuplicateLogin;

        var hashedPassword = _passwordHasherService.GeneratePasswordHash(command.Password);
        
        var user = User.Create(
            command.Login,
            command.FirstName,
            command.LastName,
            command.Email,
            command.Location,
            hashedPassword);
        
        _userRepository.Add(user);

        var token = _jwtTokenGenerator.GenerateToken(user);
        var refresh = user.GenerateRefreshToken();
        
        _userRepository.SaveChanges();
        
        return new AuthenticationResult(
            user,
            token,
            refresh.Value);
    }
}