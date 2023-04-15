using ErrorOr;
using MediatR;
using ProCardsNew.Application.Common.Interfaces.Persistence;
using ProCardsNew.Domain.Common.Errors;
using ProCardsNew.Domain.DeckAggregate.ValueObjects;
using ProCardsNew.Domain.UserAggregate.ValueObjects;

namespace ProCardsNew.Application.Service.Images.Queries.CardImage;

public class CardImageQueryHandler
    : IRequestHandler<CardImageQuery, ErrorOr<CardImageQueryResult>>
{
    private readonly IUserRepository _userRepository;
    private readonly ICardRepository _cardRepository;
    private readonly IDeckRepository _deckRepository;
    private readonly IImageRepository _imageRepository;

    public CardImageQueryHandler(
        IUserRepository userRepository,
        ICardRepository cardRepository,
        IDeckRepository deckRepository,
        IImageRepository imageRepository)
    {
        _userRepository = userRepository;
        _cardRepository = cardRepository;
        _deckRepository = deckRepository;
        _imageRepository = imageRepository;
    }
    
    public async Task<ErrorOr<CardImageQueryResult>> Handle(CardImageQuery query, CancellationToken cancellationToken)
    {
        if (await _cardRepository.GetSideByNameAsync(query.Side) is not { } side)
            return Errors.Side.NotFound;
        
        if (await _userRepository.GetByIdAsync(UserId.Create(query.UserId)) is not { } user)
            return Errors.User.NotFound;

        if (await _deckRepository.GetByIdAsync(DeckId.Create(query.DeckId)) is not { } deck)
            return Errors.Deck.NotFound;

        // TODO: check if user have access to image
        throw new NotImplementedException();

        // if (await _imageRepository.GetByCardIdAndSide(card.Id, query.Side) is not { } image)
        //     return Errors.Image.NotFound;
    }
}