using FluentValidation;
using Microsoft.Extensions.Options;
using ProCardsNew.Application.Common.Settings;

namespace ProCardsNew.Application.Service.Images.Commands.DeleteCardImage;

public class DeleteCardImageCommandValidator
    : AbstractValidator<DeleteCardImageCommand>
{
    public DeleteCardImageCommandValidator(IOptions<ValidationSettings> settings)
    {
        var validationsSettings = settings.Value;
        RuleFor(c => c.CardId)
            .NotEmpty();

        RuleFor(c => c.UserId)
            .NotEmpty();

        RuleFor(c => c.Side)
            .NotEmpty()
            .MaximumLength(validationsSettings.SideNameLength);
    }
}