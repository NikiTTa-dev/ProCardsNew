using ErrorOr;
using MediatR;
using Microsoft.Extensions.Logging;
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
    private readonly ILogger<PasswordRecoveryCommandHandler> _logger;
    private readonly PasswordRecoveryCodeSettings _recoveryCodeSettings;

    public PasswordRecoveryCommandHandler(
        IEmailSender emailSender,
        IUserRepository userRepository,
        IRandomNumberGeneratorService randomNumberGeneratorService,
        IOptions<PasswordRecoveryCodeSettings> recoveryCodeSettings,
        ILogger<PasswordRecoveryCommandHandler> logger)
    {
        _emailSender = emailSender;
        _userRepository = userRepository;
        _randomNumberGeneratorService = randomNumberGeneratorService;
        _logger = logger;
        _recoveryCodeSettings = recoveryCodeSettings.Value;
    }
    
    public async Task<ErrorOr<PasswordRecoveryResult>> Handle(
        PasswordRecoveryCommand command,
        CancellationToken cancellationToken)
    {
        if (await _userRepository.GetByLoginAsync(command.Login.ToUpper()) is not { } user)
            return new PasswordRecoveryResult();

        if (user.PasswordRecoveryLastEmailSentDateTime != null 
            && user.PasswordRecoveryLastEmailSentDateTime.Value
                .AddMinutes(_recoveryCodeSettings.EmailLockoutInMinutes)
            > DateTime.UtcNow)
            return Errors.Email.EmailAlreadySent;
        
        user.DeletePasswordRecoveryCode();
        user.SetPasswordRecoveryCode(
            _randomNumberGeneratorService
                .GeneratePasswordRecoveryCode()
                .ToString("000000"),
            _recoveryCodeSettings.ExpirationMinutes);

        _logger.Log(LogLevel.Information, "Sending email");
        user.SetEmailSendingTimeout();

        var result = await _emailSender.SendEmailAsync(
            user.Email,
            user.PasswordRecoveryCode!,
            "RecoveryCode");
        
        if (result == EmailResult.Failure)
            return Errors.Email.EmailSendingFailure;
        
        await _userRepository.SaveChangesAsync();
        
        _logger.Log(LogLevel.Information, "Email sent");

        return new PasswordRecoveryResult();
    }
}