using ErrorOr;
using MediatR;
using Microsoft.Extensions.Options;
using ProCardsNew.Application.Account.PasswordRecovery.Common;
using ProCardsNew.Application.Common.Interfaces.Persistence;
using ProCardsNew.Application.Common.Interfaces.Services;
using ProCardsNew.Application.Common.Settings;
using ProCardsNew.Domain.Common.Errors;

namespace ProCardsNew.Application.Account.PasswordRecovery.Commands.PasswordRecoveryCode;

public class PasswordRecoveryCodeCommandHandler
    : IRequestHandler<PasswordRecoveryCodeCommand, ErrorOr<PasswordRecoveryResult>>
{
    private readonly IUserRepository _userRepository;
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly LockoutSettings _lockoutSettings;

    public PasswordRecoveryCodeCommandHandler(
        IUserRepository userRepository,
        IDateTimeProvider dateTimeProvider,
        IOptions<LockoutSettings> lockoutSettings)
    {
        _userRepository = userRepository;
        _dateTimeProvider = dateTimeProvider;
        _lockoutSettings = lockoutSettings.Value;
    }
    
    public async Task<ErrorOr<PasswordRecoveryResult>> Handle(PasswordRecoveryCodeCommand command, CancellationToken cancellationToken)
    {
        if (await _userRepository.GetUserByEmailAsync(command.Email.ToUpper()) is not { } user)
            return Errors.User.NotFound;

        if (user.PasswordRecoveryCode == null ||
            user.PasswordRecoveryEndDateTime == null)
            return Errors.User.WrongRecoveryCode;
        
        if (user.PasswordRecoveryEndDateTime < _dateTimeProvider.UtcNow)
            return Errors.User.RecoveryCodeExpired;

        if (user.PasswordRecoveryCode == command.Code) 
            return new PasswordRecoveryResult();
        
        user.PasswordRecoveryFail(_lockoutSettings.PasswordRecoveryFailMaxCountInclusive);
        await _userRepository.SaveChangesAsync();
        return Errors.User.WrongRecoveryCode;
    }
}