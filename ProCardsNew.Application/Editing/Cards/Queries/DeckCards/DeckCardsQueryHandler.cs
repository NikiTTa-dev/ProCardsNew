using ErrorOr;
using MediatR;
using ProCardsNew.Application.Common.Interfaces.Persistence;
using ProCardsNew.Domain.Common.Errors;
using ProCardsNew.Domain.DeckAggregate.ValueObjects;
using ProCardsNew.Domain.UserAggregate.ValueObjects;

namespace ProCardsNew.Application.Editing.Cards.Queries.DeckCards;

public class DeckCardsQueryHandler
    : IRequestHandler<DeckCardsQuery, ErrorOr<DeckCardsQueryResult>>
{
    private readonly IUserRepository _userRepository;
    private readonly ICardRepository _cardRepository;
    private readonly IDeckRepository _deckRepository;

    public DeckCardsQueryHandler(
        IUserRepository userRepository,
        ICardRepository cardRepository,
        IDeckRepository deckRepository)
    {
        _userRepository = userRepository;
        _cardRepository = cardRepository;
        _deckRepository = deckRepository;
    }

    public async Task<ErrorOr<DeckCardsQueryResult>> Handle(DeckCardsQuery query, CancellationToken cancellationToken)
    {
        var userId = UserId.Create(query.UserId);
        if (await _userRepository.GetByIdAsync(userId) is not { } user)
            return Errors.User.NotFound;

        if (await _deckRepository.GetByIdAsync(DeckId.Create(query.DeckId)) is not { } deck)
            return Errors.Deck.NotFound;

        if (deck.OwnerId != user.Id)
            return Errors.User.AccessDenied;

        var cards = await _cardRepository.GetByOwnerIdAndDeckIdWhereAsync(
            userId: userId,
            deckId: deck.Id,
            filter: c =>
                c.FrontSide
                    .ToUpper()
                    .Contains(query.SearchQuery.ToUpper())
                || c.BackSide
                    .ToUpper()
                    .Contains(query.SearchQuery.ToUpper()),
            orderByDesc: c => c.UpdatedAtDateTime);

        var cardResults = cards
                .Select(c => new CardResult(
                    Id: c.Id.Value,
                    FrontSide: c.FrontSide,
                    BackSide: c.BackSide,
                    HasFrontImage: c.FrontImage != null,
                    HasBackImage: c.BackImage != null));

        return new DeckCardsQueryResult(
            DeckName: deck.Name,
            cardResults.ToList());
    }
}