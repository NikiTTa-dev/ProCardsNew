using FluentValidation;

namespace ProCardsNew.Application.Editing.Decks.Commands.DeleteDeck;

public class DeleteDeckCommandValidator
    : AbstractValidator<DeleteDeckCommand>
{
    public DeleteDeckCommandValidator()
    {
        RuleFor(c => c.DeckId)
            .NotEmpty();

        RuleFor(c => c.UserId)
            .NotEmpty();
    }
}