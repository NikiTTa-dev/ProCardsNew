using FluentValidation;
using Microsoft.Extensions.Options;
using ProCardsNew.Application.Common.Settings;
using ProCardsNew.Application.Common.Validators;

namespace ProCardsNew.Application.Account.Authentication.Commands.Register;

public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
{
    public RegisterCommandValidator(IOptions<ValidationSettings> settings)
    {
        var validationSettings = settings.Value;

        RuleFor(rc => rc.FirstName)
            .NotEmpty()
            .ContainsNoEdgeSpaces()
            .ContainsNoMultipleSpaces()
            .MaximumLength(validationSettings.UserFirstNameLength);

        RuleFor(rc => rc.LastName)
            .NotEmpty()
            .ContainsNoEdgeSpaces()
            .ContainsNoMultipleSpaces()
            .MaximumLength(validationSettings.UserLastNameLength);
        
        RuleFor(rc => rc.Email)
            .NotEmpty()
            .EmailAddress()
            .ContainsNoSpaces()
            .MaximumLength(validationSettings.UserEmailLength);
        
        RuleFor(rc => rc.Location)
            .NotEmpty()
            .ContainsNoEdgeSpaces()
            .ContainsNoMultipleSpaces()
            .MaximumLength(validationSettings.UserLocationLength);
        
        RuleFor(rc => rc.Login)
            .NotEmpty()
            .ContainsNoSpaces()
            .MaximumLength(validationSettings.UserLoginLength);

        RuleFor(rc => rc.Password)
            .NotEmpty()
            .Password(
                validationSettings.UserPasswordMinLength,
                validationSettings.UserPasswordMaxLength);
    }
}