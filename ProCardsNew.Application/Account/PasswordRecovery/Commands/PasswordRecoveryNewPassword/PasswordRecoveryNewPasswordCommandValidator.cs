using FluentValidation;
using Microsoft.Extensions.Options;
using ProCardsNew.Application.Common.Settings;
using ProCardsNew.Application.Common.Validators;

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
            .ContainsNoSpaces()
            .MaximumLength(validationSettings.UserEmailLength);

        RuleFor(c => c.Code)
            .NotEmpty()
            .ContainsNoSpaces()
            .ContainsNumbersOnly()
            .MaximumLength(validationSettings.UserPasswordRecoveryCodeLength);

        RuleFor(c => c.Password)
            .NotEmpty()
            .Password(
                validationSettings.UserPasswordMinLength,
                validationSettings.UserPasswordMaxLength);
    }
}