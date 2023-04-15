using FluentValidation;
using Microsoft.Extensions.Options;
using ProCardsNew.Application.Common.Settings;
using ProCardsNew.Application.Common.Validators;

namespace ProCardsNew.Application.Account.Profile.Commands.EditPassword;

public class EditPasswordCommandValidator
    : AbstractValidator<EditPasswordCommand>
{
    public EditPasswordCommandValidator(IOptions<ValidationSettings> settings)
    {
        var validationSettings = settings.Value;

        RuleFor(c => c.UserId)
            .NotEmpty();

        RuleFor(c => c.OldPassword)
            .NotEmpty()
            .ContainsNoSpaces()
            .MaximumLength(validationSettings.UserPasswordMaxLength);

        RuleFor(c => c.NewPassword)
            .NotEmpty()
            .Password(
                validationSettings.UserPasswordMinLength,
                validationSettings.UserPasswordMaxLength);
    }
}