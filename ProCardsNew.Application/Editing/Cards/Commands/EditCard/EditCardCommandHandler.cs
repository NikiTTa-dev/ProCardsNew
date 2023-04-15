using ErrorOr;
using MediatR;
using ProCardsNew.Application.Common.Interfaces.Persistence;
using ProCardsNew.Domain.CardAggregate.ValueObjects;
using ProCardsNew.Domain.Common.Errors;
using ProCardsNew.Domain.UserAggregate.ValueObjects;

namespace ProCardsNew.Application.Editing.Cards.Commands.EditCard;

public class EditCardCommandHandler
    : IRequestHandler<EditCardCommand, ErrorOr<EditCardCommandResult>>
{
    private readonly IUserRepository _userRepository;
    private readonly ICardRepository _cardRepository;

    public EditCardCommandHandler(
        IUserRepository userRepository,
        ICardRepository cardRepository)
    {
        _userRepository = userRepository;
        _cardRepository = cardRepository;
    }

    public async Task<ErrorOr<EditCardCommandResult>> Handle(
        EditCardCommand command,
        CancellationToken cancellationToken)
    {
        if (await _userRepository.GetByIdAsync(UserId.Create(command.UserId)) is not { } user)
            return Errors.User.NotFound;

        if (await _cardRepository.GetByIdAsync(CardId.Create(command.CardId)) is not { } card)
            return Errors.Card.NotFound;

        if (card.OwnerId != user.Id)
            return Errors.User.AccessDenied;

        card.Edit(command.FrontSide, command.BackSide);
        await _cardRepository.SaveChangesAsync();
        
        return new EditCardCommandResult();
    }
}