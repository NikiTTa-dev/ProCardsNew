using ErrorOr;
using MediatR;
using ProCardsNew.Application.Common.Interfaces.Persistence;
using ProCardsNew.Domain.CardAggregate.ValueObjects;
using ProCardsNew.Domain.Common.Errors;
using ProCardsNew.Domain.DeckAggregate.ValueObjects;
using ProCardsNew.Domain.UserAggregate.ValueObjects;

namespace ProCardsNew.Application.Editing.Decks.Commands.RemoveCardFromDeck;

public class RemoveCardFromDeckCommandHandler 
    : IRequestHandler<RemoveCardFromDeckCommand, ErrorOr<RemoveCardFromDeckCommandResult>>
{
    private readonly IUserRepository _userRepository;
    private readonly ICardRepository _cardRepository;
    private readonly IDeckRepository _deckRepository;

    public RemoveCardFromDeckCommandHandler(
        IUserRepository userRepository,
        ICardRepository cardRepository,
        IDeckRepository deckRepository)
    {
        _userRepository = userRepository;
        _cardRepository = cardRepository;
        _deckRepository = deckRepository;
    }
    
    public async Task<ErrorOr<RemoveCardFromDeckCommandResult>> Handle(RemoveCardFromDeckCommand request, CancellationToken cancellationToken)
    {
        if (await _userRepository.GetByIdAsync(UserId.Create(request.UserId)) is not { } user)
            return Errors.User.NotFound;

        if (await _deckRepository.GetByIdAsync(DeckId.Create(request.DeckId)) is not { } deck)
            return Errors.Deck.NotFound;
        
        if (await _cardRepository.GetByIdAsync(CardId.Create(request.CardId)) is not { } card)
            return Errors.Card.NotFound;

        if (deck.OwnerId != user.Id)
            return Errors.User.AccessDenied;
        
        if (!await _deckRepository.HasCard(deck.Id, card.Id))
            return Errors.Card.NotFound;

        await _deckRepository.RemoveCardFromDeck(deck.Id, card.Id);
        await _deckRepository.SaveChangesAsync();

        return new RemoveCardFromDeckCommandResult();
    }
}