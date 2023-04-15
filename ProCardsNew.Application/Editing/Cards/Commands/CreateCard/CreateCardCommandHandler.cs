using ErrorOr;
using MediatR;
using ProCardsNew.Application.Common.Interfaces.Persistence;
using ProCardsNew.Domain.CardAggregate;
using ProCardsNew.Domain.Common.Errors;
using ProCardsNew.Domain.DeckAggregate.ValueObjects;
using ProCardsNew.Domain.UserAggregate.ValueObjects;

namespace ProCardsNew.Application.Editing.Cards.Commands.CreateCard;

public class CreateCardCommandHandler
    : IRequestHandler<CreateCardCommand, ErrorOr<CreateCardCommandResult>>
{
    private readonly IUserRepository _userRepository;
    private readonly IDeckRepository _deckRepository;

    public CreateCardCommandHandler(
        IUserRepository userRepository,
        IDeckRepository deckRepository)
    {
        _userRepository = userRepository;
        _deckRepository = deckRepository;
    }
    
    public async Task<ErrorOr<CreateCardCommandResult>> Handle(CreateCardCommand command, CancellationToken cancellationToken)
    {
        var userId = UserId.Create(command.UserId);
        if (await _userRepository.GetByIdAsync(userId) is not {} user)
            return Errors.User.NotFound;

        if (await _deckRepository.GetByIdAsync(DeckId.Create(command.DeckId)) is not { } deck)
            return Errors.Deck.NotFound;

        if (deck.OwnerId != user.Id)
            return Errors.User.AccessDenied;

        var card = Card.Create(
            ownerId: userId,
            frontSide: command.FrontSide,
            backSide: command.BackSide);

        _deckRepository.ChangeStateToAdd(deck.AddCard(card));
        await _deckRepository.SaveChangesAsync();
        
        return new CreateCardCommandResult(card.Id.Value);
    }
}