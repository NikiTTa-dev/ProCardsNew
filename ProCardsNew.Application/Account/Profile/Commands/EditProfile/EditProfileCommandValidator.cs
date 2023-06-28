using FluentValidation;
using Microsoft.Extensions.Options;
using ProCardsNew.Application.Common.Settings;
using ProCardsNew.Application.Common.Validators;

namespace ProCardsNew.Application.Account.Profile.Commands.EditProfile;

public class EditProfileCommandValidator
    : AbstractValidator<EditProfileCommand>
{
    public EditProfileCommandValidator(IOptions<ValidationSettings> settings)
    {
        var validationSettings = settings.Value;

        RuleFor(c => c.UserId)
            .NotEmpty();
        
        RuleFor(c => c.FirstName)
            .NotEmpty()
            .ContainsNoEdgeSpaces()
            .ContainsNoMultipleSpaces()
            .MaximumLength(validationSettings.UserFirstNameLength);

        RuleFor(c => c.LastName)
            .NotEmpty()
            .ContainsNoEdgeSpaces()
            .ContainsNoMultipleSpaces()
            .MaximumLength(validationSettings.UserLastNameLength);
        
        RuleFor(c => c.Email)
            .NotEmpty()
            .EmailAddress()
            .ContainsNoSpaces()
            .MaximumLength(validationSettings.UserEmailLength);
        
        RuleFor(c => c.Location)
            .NotEmpty()
            .ContainsNoEdgeSpaces()
            .ContainsNoMultipleSpaces()
            .MaximumLength(validationSettings.UserLocationLength);

        RuleFor(c => c.AvatarNumber)
            .GreaterThan(-1)
            .LessThanOrEqualTo(validationSettings.UserAvatarsCount - 1);
    }
}