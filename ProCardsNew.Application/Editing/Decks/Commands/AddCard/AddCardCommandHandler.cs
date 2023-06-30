using ErrorOr;
using MediatR;
using ProCardsNew.Application.Common.Interfaces.Persistence;
using ProCardsNew.Domain.CardAggregate.ValueObjects;
using ProCardsNew.Domain.Common.Errors;
using ProCardsNew.Domain.DeckAggregate.ValueObjects;
using ProCardsNew.Domain.UserAggregate.ValueObjects;

namespace ProCardsNew.Application.Editing.Decks.Commands.AddCard;

public class AddCardCommandHandler 
    : IRequestHandler<AddCardCommand, ErrorOr<AddCardCommandResult>>
{
    private readonly IUserRepository _userRepository;
    private readonly ICardRepository _cardRepository;
    private readonly IDeckRepository _deckRepository;

    public AddCardCommandHandler(
        IUserRepository userRepository,
        ICardRepository cardRepository,
        IDeckRepository deckRepository)
    {
        _userRepository = userRepository;
        _cardRepository = cardRepository;
        _deckRepository = deckRepository;
    }
    
    public async Task<ErrorOr<AddCardCommandResult>> Handle(AddCardCommand request, CancellationToken cancellationToken)
    {
        if (await _userRepository.GetByIdAsync(UserId.Create(request.UserId)) is not { } user)
            return Errors.User.NotFound;

        if (await _deckRepository.GetByIdAsync(DeckId.Create(request.DeckId)) is not { } deck)
            return Errors.Deck.NotFound;

        if (deck.OwnerId != user.Id)
            return Errors.User.AccessDenied;

        if (await _cardRepository.GetByIdAsync(CardId.Create(request.CardId)) is not { } card)
            return Errors.Card.NotFound;
        
        if (card.OwnerId != user.Id)
            return Errors.User.AccessDenied;

        if (await _deckRepository.HasCard(deck.Id, card.Id))
            return Errors.Deck.DuplicateCard;

        _deckRepository.ChangeStateToAdded( deck.AddCard(card));
        await _deckRepository.SaveChangesAsync();

        return new AddCardCommandResult();
    }
}