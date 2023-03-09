using ErrorOr;
using MediatR;
using ProCardsNew.Application.Account.Authentication.Common;
using ProCardsNew.Application.Common.Interfaces.Authentication;
using ProCardsNew.Application.Common.Interfaces.Repositories;
using ProCardsNew.Domain.UserAggregate;

namespace ProCardsNew.Application.Account.Authentication.Commands.Register;

public class RegisterCommandHandler:
    IRequestHandler<RegisterCommand, ErrorOr<AuthenticationResult>>
{
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IUserRepository _userRepository;

    public RegisterCommandHandler(IJwtTokenGenerator jwtTokenGenerator, IUserRepository userRepository)
    {
        _jwtTokenGenerator = jwtTokenGenerator;
        _userRepository = userRepository;
    }

    public async Task<ErrorOr<AuthenticationResult>> Handle(RegisterCommand command, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        
        if (_userRepository.GetUserByLogin(command.Login) is not null)
            return Error.Conflict();

        var user = User.Create(
            command.Login,
            command.FirstName,
            command.LastName,
            command.Email,
            command.Location,
            command.Password);
        
        _userRepository.Add(user);

        var token = _jwtTokenGenerator.GenerateToken(user);

        return new AuthenticationResult(
            user,
            token);
    }
}