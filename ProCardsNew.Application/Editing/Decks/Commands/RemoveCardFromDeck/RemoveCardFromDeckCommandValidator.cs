using FluentValidation;

namespace ProCardsNew.Application.Editing.Decks.Commands.RemoveCardFromDeck;

public class RemoveCardFromDeckCommandValidator : AbstractValidator<RemoveCardFromDeckCommand>
{
    public RemoveCardFromDeckCommandValidator()
    {
        RuleFor(c => c.CardId)
            .NotEmpty();
        
        RuleFor(c => c.DeckId)
            .NotEmpty();
        
        RuleFor(c => c.UserId)
            .NotEmpty();
    }
}