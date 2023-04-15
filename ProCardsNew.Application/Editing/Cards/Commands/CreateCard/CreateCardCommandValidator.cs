using FluentValidation;
using Microsoft.Extensions.Options;
using ProCardsNew.Application.Common.Settings;
using ProCardsNew.Application.Common.Validators;

namespace ProCardsNew.Application.Editing.Cards.Commands.CreateCard;

public class CreateCardCommandValidator
    : AbstractValidator<CreateCardCommand>
{
    public CreateCardCommandValidator(IOptions<ValidationSettings> settings)
    {
        var validationSettings = settings.Value;

        RuleFor(q => q.UserId)
            .NotEmpty();

        RuleFor(q => q.DeckId)
            .NotEmpty();

        RuleFor(q => q.FrontSide)
            .NotEmpty()
            .ContainsNoEdgeSpaces()
            .ContainsNoMultipleSpaces()
            .MinimumLength(validationSettings.CardSideMinLength)
            .MaximumLength(validationSettings.CardSideMaxLength);
        
        RuleFor(q => q.BackSide)
            .NotEmpty()
            .ContainsNoEdgeSpaces()
            .ContainsNoMultipleSpaces()
            .MinimumLength(validationSettings.CardSideMinLength)
            .MaximumLength(validationSettings.CardSideMaxLength);
    }
}