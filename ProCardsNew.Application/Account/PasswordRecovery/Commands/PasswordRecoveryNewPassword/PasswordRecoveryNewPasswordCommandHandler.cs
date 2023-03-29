using ErrorOr;
using MediatR;
using ProCardsNew.Application.Account.PasswordRecovery.Common;
using ProCardsNew.Application.Common.Interfaces.Authentication;
using ProCardsNew.Application.Common.Interfaces.Persistence;
using ProCardsNew.Application.Common.Interfaces.Services;
using ProCardsNew.Domain.Common.Errors;

namespace ProCardsNew.Application.Account.PasswordRecovery.Commands.PasswordRecoveryNewPassword;

public class PasswordRecoveryNewPasswordCommandHandler
    : IRequestHandler<PasswordRecoveryNewPasswordCommand, ErrorOr<PasswordRecoveryResult>>
{
    private readonly IUserRepository _userRepository;
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly IPasswordHasherService _passwordHasherService;

    public PasswordRecoveryNewPasswordCommandHandler(
        IUserRepository userRepository,
        IDateTimeProvider dateTimeProvider,
        IPasswordHasherService passwordHasherService)
    {
        _userRepository = userRepository;
        _dateTimeProvider = dateTimeProvider;
        _passwordHasherService = passwordHasherService;
    }
    
    public async Task<ErrorOr<PasswordRecoveryResult>> Handle(
        PasswordRecoveryNewPasswordCommand command,
        CancellationToken cancellationToken)
    {
        if (await _userRepository.GetUserByEmailAsync(command.Email.ToUpper()) is not { } user)
            return Errors.User.NotFound;
        
        if (user.PasswordRecoveryCode == null ||
            user.PasswordRecoveryEndDateTime == null)
            return Errors.User.WrongRecoveryCode;
        
        if (user.PasswordRecoveryEndDateTime < _dateTimeProvider.UtcNow)
            return Errors.User.RecoveryCodeExpired;

        if (user.PasswordRecoveryCode != command.Code)
            return Errors.User.WrongRecoveryCode;
        
        user.DeletePasswordRecoveryCode();
        user.RehashPassword(_passwordHasherService.GeneratePasswordHash(command.Password));

        await _userRepository.SaveChangesAsync();

        return new PasswordRecoveryResult();
    }
}