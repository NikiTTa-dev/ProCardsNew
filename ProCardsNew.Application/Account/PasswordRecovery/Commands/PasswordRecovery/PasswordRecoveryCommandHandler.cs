using ErrorOr;
using MediatR;
using Microsoft.Extensions.Options;
using ProCardsNew.Application.Account.PasswordRecovery.Common;
using ProCardsNew.Application.Common.Enums;
using ProCardsNew.Application.Common.Interfaces.Persistence;
using ProCardsNew.Application.Common.Interfaces.Services;
using ProCardsNew.Application.Common.Settings;
using ProCardsNew.Domain.Common.Errors;

namespace ProCardsNew.Application.Account.PasswordRecovery.Commands.PasswordRecovery;

public class PasswordRecoveryCommandHandler:
    IRequestHandler<PasswordRecoveryCommand, ErrorOr<PasswordRecoveryResult>>
{
    private readonly IEmailSender _emailSender;
    private readonly IUserRepository _userRepository;
    private readonly IRandomNumberGeneratorService _randomNumberGeneratorService;
    private readonly PasswordRecoveryCodeSettings _recoveryCodeSettings;

    public PasswordRecoveryCommandHandler(
        IEmailSender emailSender,
        IUserRepository userRepository,
        IRandomNumberGeneratorService randomNumberGeneratorService,
        IOptions<PasswordRecoveryCodeSettings> recoveryCodeSettings)
    {
        _emailSender = emailSender;
        _userRepository = userRepository;
        _randomNumberGeneratorService = randomNumberGeneratorService;
        _recoveryCodeSettings = recoveryCodeSettings.Value;
    }
    
    public async Task<ErrorOr<PasswordRecoveryResult>> Handle(PasswordRecoveryCommand command, CancellationToken cancellationToken)
    {
        if (await _userRepository.GetUserByEmailAsync(command.Email.ToUpper()) is not { } user)
            return new PasswordRecoveryResult();
        
        user.SetPasswordRecoveryCode(
            _randomNumberGeneratorService
                .GeneratePasswordRecoveryCode()
                .ToString("000000"),
            _recoveryCodeSettings.ExpirationMinutes);

        await _userRepository.SaveChangesAsync();
        
        var result = await _emailSender.SendEmailAsync(
            command.Email,
            user.PasswordRecoveryCode!,
            "RecoveryCode");

        if (result == EmailResult.Failure)
            return Errors.Email.EmailSendingFailure;
        
        return new PasswordRecoveryResult();
    }
}