using ErrorOr;
using MediatR;
using ProCardsNew.Application.Common.Interfaces.Persistence;
using ProCardsNew.Application.Editing.Cards.Queries.DeckCards;
using ProCardsNew.Domain.Common.Errors;
using ProCardsNew.Domain.UserAggregate.ValueObjects;

namespace ProCardsNew.Application.Editing.Cards.Queries.UserCards;

public class UserCardsQueryHandler
    : IRequestHandler<UserCardsQuery, ErrorOr<UserCardsQueryResult>>
{
    private readonly IUserRepository _userRepository;
    private readonly ICardRepository _cardRepository;

    public UserCardsQueryHandler(
        IUserRepository userRepository,
        ICardRepository cardRepository)
    {
        _userRepository = userRepository;
        _cardRepository = cardRepository;
    }

    public async Task<ErrorOr<UserCardsQueryResult>> Handle(UserCardsQuery query, CancellationToken cancellationToken)
    {
        if (await _userRepository.GetByIdAsync(UserId.Create(query.UserId)) is not { } user)
            return Errors.User.NotFound;

        var cards = await _cardRepository.GetByOwnerIdWhereAsync(
            user.Id,
            c => 
                c.FrontSide
                    .ToUpper()
                    .Contains(query.SearchQuery.ToUpper())
                || c.BackSide
                    .ToUpper()
                    .Contains(query.SearchQuery.ToUpper()),
            c => c.UpdatedAtDateTime);

        return new UserCardsQueryResult(
            cards.ConvertAll(c
                => new CardResult(
                    Id: c.Id.Value,
                    FrontSide: c.FrontSide,
                    BackSide: c.BackSide,
                    HasFrontImage: c.FrontImage != null,
                    HasBackImage: c.BackImage != null)));
    }
}