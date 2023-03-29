using System.Text.RegularExpressions;
using FluentValidation;
using Microsoft.Extensions.Options;
using ProCardsNew.Application.Common.Settings;

namespace ProCardsNew.Application.Account.Authentication.Commands.Register;

public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
{
    public RegisterCommandValidator(IOptions<ValidationSettings> settings)
    {
        var validationSettings = settings.Value;

        RuleFor(rc => rc.FirstName)
            .NotEmpty()
            .MaximumLength(validationSettings.UserFirstNameLength);

        RuleFor(rc => rc.LastName)
            .NotEmpty()
            .MaximumLength(validationSettings.UserLastNameLength);
        
        RuleFor(rc => rc.Email)
            .NotEmpty()
            .EmailAddress()
            .MaximumLength(validationSettings.UserEmailLength);
        
        RuleFor(rc => rc.Location)
            .NotEmpty()
            .MaximumLength(validationSettings.UserLocationLength);
        
        RuleFor(rc => rc.Login)
            .NotEmpty()
            .MaximumLength(validationSettings.UserLoginLength);
        
        RuleFor(rc => rc.Password)
            .NotEmpty()
            .MinimumLength(validationSettings.UserPasswordMinLength)
            .MaximumLength(validationSettings.UserPasswordMaxLength)
            .Must(p => 
                Regex.Match(p, @"[a-z]", RegexOptions.ECMAScript).Success &&
                Regex.Match(p, @"[A-Z]", RegexOptions.ECMAScript).Success);
    }
}