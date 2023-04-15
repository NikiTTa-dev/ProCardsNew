using FluentValidation;

namespace ProCardsNew.Application.Editing.Cards.Commands.DeleteCard;

public class DeleteCardCommandValidator
    : AbstractValidator<DeleteCardCommand>
{
    public DeleteCardCommandValidator()
    {
        RuleFor(c => c.UserId)
            .NotEmpty();

        RuleFor(c => c.CardId)
            .NotEmpty();
    }
}