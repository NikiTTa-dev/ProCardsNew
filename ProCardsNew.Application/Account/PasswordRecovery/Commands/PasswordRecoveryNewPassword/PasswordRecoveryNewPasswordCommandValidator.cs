using System.Text.RegularExpressions;
using FluentValidation;
using Microsoft.Extensions.Options;
using ProCardsNew.Application.Common.Settings;

namespace ProCardsNew.Application.Account.PasswordRecovery.Commands.PasswordRecoveryNewPassword;

public class PasswordRecoveryNewPasswordValidator 
    : AbstractValidator<PasswordRecoveryNewPasswordCommand>
{
    public PasswordRecoveryNewPasswordValidator(IOptions<ValidationSettings> settings)
    {
        var validationSettings = settings.Value;

        RuleFor(c => c.Email)
            .NotEmpty()
            .EmailAddress()
            .MaximumLength(validationSettings.UserEmailLength);

        RuleFor(c => c.Code)
            .NotEmpty()
            .MaximumLength(validationSettings.UserRecoveryCodeLength);
        
        RuleFor(c => c.Password)
            .NotEmpty()
            .MinimumLength(validationSettings.UserPasswordMinLength)
            .MaximumLength(validationSettings.UserPasswordMaxLength)
            .Must(p => 
                Regex.Match(p, @"[a-z]", RegexOptions.ECMAScript).Success &&
                Regex.Match(p, @"[A-Z]", RegexOptions.ECMAScript).Success);
    }
}