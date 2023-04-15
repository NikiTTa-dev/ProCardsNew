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
        UserId.Create(command.UserId);
        var user = await _userRepository.GetByIdIncludeAsync(
            UserId.Create(command.UserId),
            u => u.Statistic);
        
        if (user is null)
            return Errors.User.NotFound;

        if (await _deckRepository.GetByIdAsync(DeckId.Create(command.DeckId)) is not { } deck)
            return Errors.Deck.NotFound;

        if (deck.OwnerId != user.Id)
            return Errors.User.AccessDenied;

        var card = Card.Create(
            ownerId: user.Id,
            frontSide: command.FrontSide,
            backSide: command.BackSide);

        user.AddCard(card);
        _deckRepository.ChangeStateToAdd(deck.AddCard(card));
        await _deckRepository.SaveChangesAsync();
        
        return new CreateCardCommandResult(card.Id.Value);
    }
}