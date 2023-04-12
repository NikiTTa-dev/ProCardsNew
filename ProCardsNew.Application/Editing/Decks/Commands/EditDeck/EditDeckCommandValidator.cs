using FluentValidation;
using Microsoft.Extensions.Options;
using ProCardsNew.Application.Common.Settings;
using ProCardsNew.Application.Common.Validators;

namespace ProCardsNew.Application.Editing.Decks.Commands.EditDeck;

public class EditDeckCommandValidator : AbstractValidator<EditDeckCommand>
{
    public EditDeckCommandValidator(IOptions<ValidationSettings> settings)
    {
        var validationSettings = settings.Value;

        RuleFor(c => c.UserId)
            .NotEmpty();

        RuleFor(c => c.DeckId)
            .NotEmpty();

        RuleFor(c => c.Name)
            .NotEmpty()
            .ContainsNoEdgeSpaces()
            .ContainsNoMultipleSpaces()
            .MaximumLength(validationSettings.DeckNameLength);

        RuleFor(c => c.Description)
            .NotEmpty()
            .ContainsNoEdgeSpaces()
            .ContainsNoMultipleSpaces()
            .MaximumLength(validationSettings.DeckDescriptionLength);
    }
}