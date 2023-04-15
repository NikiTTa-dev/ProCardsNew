using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Identity;
using ProCardsNew.Application.Common.Interfaces.Authentication;
using ProCardsNew.Application.Common.Interfaces.Persistence;
using ProCardsNew.Domain.Common.Errors;
using ProCardsNew.Domain.UserAggregate.ValueObjects;

namespace ProCardsNew.Application.Account.Profile.Commands.EditPassword;

public class EditPasswordCommandHandler
    : IRequestHandler<EditPasswordCommand, ErrorOr<EditPasswordCommandResult>>
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasherService _passwordHasherService;

    public EditPasswordCommandHandler(
        IUserRepository userRepository,
        IPasswordHasherService passwordHasherService)
    {
        _userRepository = userRepository;
        _passwordHasherService = passwordHasherService;
    }
    
    public async Task<ErrorOr<EditPasswordCommandResult>> Handle(EditPasswordCommand command, CancellationToken cancellationToken)
    {
        if (await _userRepository.GetByIdAsync(UserId.Create(command.UserId)) is not { } user)
            return Errors.User.NotFound;

        var hashedPassword = _passwordHasherService.GeneratePasswordHash(command.NewPassword);
        
        if (_passwordHasherService
                .VerifyPasswordHash(hashedPassword, command.OldPassword)
            is PasswordVerificationResult.Failed)
            return Errors.Authentication.InvalidCredentials;
        
        user.RehashPassword(hashedPassword);
        await _userRepository.SaveChangesAsync();
        
        return new EditPasswordCommandResult();
    }
}