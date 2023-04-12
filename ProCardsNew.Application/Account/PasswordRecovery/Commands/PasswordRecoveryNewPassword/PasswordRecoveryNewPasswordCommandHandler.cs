using ErrorOr;
using MediatR;
using Microsoft.Extensions.Options;
using ProCardsNew.Application.Account.PasswordRecovery.Common;
using ProCardsNew.Application.Common.Interfaces.Authentication;
using ProCardsNew.Application.Common.Interfaces.Persistence;
using ProCardsNew.Application.Common.Interfaces.Services;
using ProCardsNew.Application.Common.Settings;
using ProCardsNew.Domain.Common.Errors;

namespace ProCardsNew.Application.Account.PasswordRecovery.Commands.PasswordRecoveryNewPassword;

public class PasswordRecoveryNewPasswordCommandHandler
    : IRequestHandler<PasswordRecoveryNewPasswordCommand, ErrorOr<PasswordRecoveryResult>>
{
    private readonly IUserRepository _userRepository;
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly IPasswordHasherService _passwordHasherService;
    private readonly LockoutSettings _lockoutSettings;

    public PasswordRecoveryNewPasswordCommandHandler(
        IUserRepository userRepository,
        IDateTimeProvider dateTimeProvider,
        IPasswordHasherService passwordHasherService,
        IOptions<LockoutSettings> lockoutSettings)
    {
        _userRepository = userRepository;
        _dateTimeProvider = dateTimeProvider;
        _passwordHasherService = passwordHasherService;
        _lockoutSettings = lockoutSettings.Value;
    }
    
    public async Task<ErrorOr<PasswordRecoveryResult>> Handle(
        PasswordRecoveryNewPasswordCommand command,
        CancellationToken cancellationToken)
    {
        if (await _userRepository.GetByEmailAsync(command.Email.ToUpper()) is not { } user)
            return Errors.User.NotFound;
        
        if (user.PasswordRecoveryCode == null ||
            user.PasswordRecoveryEndDateTime == null)
            return Errors.User.WrongRecoveryCode;
        
        if (user.PasswordRecoveryEndDateTime < _dateTimeProvider.UtcNow)
            return Errors.User.RecoveryCodeExpired;

        if (user.PasswordRecoveryCode != command.Code)
        {
            user.PasswordRecoveryFail(_lockoutSettings.PasswordRecoveryFailMaxCountInclusive);
            await _userRepository.SaveChangesAsync();
            return Errors.User.WrongRecoveryCode;
        }
        
        user.DeletePasswordRecoveryCode();
        user.RehashPassword(_passwordHasherService.GeneratePasswordHash(command.Password));

        await _userRepository.SaveChangesAsync();

        return new PasswordRecoveryResult();
    }
}