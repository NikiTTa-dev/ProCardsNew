using FluentValidation;
using Microsoft.Extensions.Options;
using ProCardsNew.Application.Common.Settings;
using ProCardsNew.Application.Common.Validators;

namespace ProCardsNew.Application.Service.Images.Commands.AddCardImage;

public class AddCardImageCommandValidator
    : AbstractValidator<AddCardImageCommand>
{
    public AddCardImageCommandValidator(IOptions<ValidationSettings> settings)
    {
        var validationSettings = settings.Value;

        RuleFor(c => c.Name)
            .NotEmpty()
            .MaximumLength(validationSettings.ImageNameLength);

        RuleFor(c => c.CardId)
            .NotEmpty();

        RuleFor(c => c.UserId)
            .NotEmpty();

        RuleFor(c => c.Side)
            .NotEmpty()
            .MaximumLength(validationSettings.SideNameLength);

        RuleFor(c => c.FileExtension)
            .NotEmpty()
            .ImageFileExtension()
            .MaximumLength(validationSettings.ImageFileExtensionLength);

        RuleFor(c => c.Data)
            .NotEmpty()
            .FileLength(validationSettings.CardImageLength);
    }
}