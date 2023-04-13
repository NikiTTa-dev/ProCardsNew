using FluentValidation;
using Microsoft.Extensions.Options;
using ProCardsNew.Application.Common.Settings;
using ProCardsNew.Application.Common.Validators;

namespace ProCardsNew.Application.Editing.Decks.Commands.EditDeckPassword;

public class EditDeckPasswordCommandValidator 
    : AbstractValidator<EditDeckPasswordCommand>
{
    public EditDeckPasswordCommandValidator(IOptions<ValidationSettings> settings)
    {
        var validationSettings = settings.Value;

        RuleFor(c => c.DeckId)
            .NotEmpty();
        
        RuleFor(c => c.UserId)
            .NotEmpty();

        RuleFor(c => c.IsPrivate)
            .NotNull();
        
        RuleFor(c => c.Password)
            .NotEmpty()
            .ContainsNoSpaces()
            .MinimumLength(validationSettings.DeckPasswordMinLength)
            .MaximumLength(validationSettings.DeckPasswordMaxLength);
    }   
}