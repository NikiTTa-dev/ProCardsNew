using FluentValidation;

namespace ProCardsNew.Application.Learning.Decks.Commands.Deck;

public class DeckCommandValidator
    : AbstractValidator<DeckCommand>
{
    public DeckCommandValidator()
    {
        RuleFor(c => c.UserId)
            .NotEmpty();

        RuleFor(c => c.DeckId)
            .NotEmpty();
    }
}