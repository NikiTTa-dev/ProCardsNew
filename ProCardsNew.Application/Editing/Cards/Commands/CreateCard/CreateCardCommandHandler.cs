using ErrorOr;
using MediatR;
using ProCardsNew.Application.Common.Interfaces.Persistence;
using ProCardsNew.Domain.CardAggregate;
using ProCardsNew.Domain.Common.Errors;
using ProCardsNew.Domain.UserAggregate.ValueObjects;

namespace ProCardsNew.Application.Editing.Cards.Commands.CreateCard;

public class CreateCardCommandHandler
    : IRequestHandler<CreateCardCommand, ErrorOr<CreateCardCommandResult>>
{
    private readonly IUserRepository _userRepository;
    private readonly IDeckRepository _deckRepository;
    private readonly ICardRepository _cardRepository;

    public CreateCardCommandHandler(
        IUserRepository userRepository,
        IDeckRepository deckRepository,
        ICardRepository cardRepository)
    {
        _userRepository = userRepository;
        _deckRepository = deckRepository;
        _cardRepository = cardRepository;
    }
    
    public async Task<ErrorOr<CreateCardCommandResult>> Handle(CreateCardCommand command, CancellationToken cancellationToken)
    {
        if (await _userRepository.GetByIdAsync(UserId.Create(command.UserId)) is not { } user)
            return Errors.User.NotFound;

        if (await _cardRepository.GetByNameAsync(user.Id, command.FrontSide, command.BackSide) is not null)
            return Errors.Card.AlreadyExists;

        var card = Card.Create(
            ownerId: user.Id,
            frontSide: command.FrontSide,
            backSide: command.BackSide);

        await _cardRepository.AddAsync(card);
        await _deckRepository.SaveChangesAsync();
        
        return new CreateCardCommandResult(card.Id.Value);
    }
}