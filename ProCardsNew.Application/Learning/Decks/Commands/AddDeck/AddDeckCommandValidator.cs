using FluentValidation;
using Microsoft.Extensions.Options;
using ProCardsNew.Application.Common.Settings;

namespace ProCardsNew.Application.Learning.Decks.Commands.AddDeck;

public class AddDeckCommandValidator
    : AbstractValidator<AddDeckCommand>
{
    public AddDeckCommandValidator(IOptions<ValidationSettings> settings)
    {
        var validationSettings = settings.Value;
        RuleFor(c => c.DeckId)
            .NotEmpty();

        RuleFor(c => c.UserId)
            .NotEmpty();

        RuleFor(c => c.Password)
            .NotEmpty()
            .MaximumLength(validationSettings.DeckPasswordMaxLength);
    }
}