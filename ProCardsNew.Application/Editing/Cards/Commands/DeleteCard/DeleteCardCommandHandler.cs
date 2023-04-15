using ErrorOr;
using MediatR;
using ProCardsNew.Application.Common.Interfaces.Persistence;
using ProCardsNew.Domain.CardAggregate.ValueObjects;
using ProCardsNew.Domain.Common.Errors;
using ProCardsNew.Domain.UserAggregate.ValueObjects;

namespace ProCardsNew.Application.Editing.Cards.Commands.DeleteCard;

public class DeleteCardCommandHandler
    : IRequestHandler<DeleteCardCommand, ErrorOr<DeleteCardCommandResult>>
{
    private readonly IUserRepository _userRepository;
    private readonly ICardRepository _cardRepository;

    public DeleteCardCommandHandler(
        IUserRepository userRepository,
        ICardRepository cardRepository)
    {
        _userRepository = userRepository;
        _cardRepository = cardRepository;
    }
    
    public async Task<ErrorOr<DeleteCardCommandResult>> Handle(DeleteCardCommand command, CancellationToken cancellationToken)
    {
        if (await _userRepository.GetByIdAsync(UserId.Create(command.UserId)) is not { } user)
            return Errors.User.NotFound;

        if (await _cardRepository.GetByIdAsync(CardId.Create(command.CardId)) is not { } card)
            return Errors.Card.NotFound;

        if (card.OwnerId != user.Id)
            return Errors.User.AccessDenied;

        _cardRepository.Delete(card);
        await _cardRepository.SaveChangesAsync();
        
        return new DeleteCardCommandResult();
    }
}