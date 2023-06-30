using FluentValidation;

namespace ProCardsNew.Application.Editing.Decks.Commands.AddCard;

public class AddCardCommandValidator: AbstractValidator<AddCardCommand>
{
    public AddCardCommandValidator()
    {
        RuleFor(c => c.CardId)
            .NotEmpty();
        
        RuleFor(c => c.DeckId)
            .NotEmpty();
        
        RuleFor(c => c.UserId)
            .NotEmpty();
    }
}