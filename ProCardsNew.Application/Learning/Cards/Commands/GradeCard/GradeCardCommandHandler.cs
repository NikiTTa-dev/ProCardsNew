using ErrorOr;
using MediatR;
using ProCardsNew.Application.Common.Interfaces.Persistence;
using ProCardsNew.Domain.CardAggregate.ValueObjects;
using ProCardsNew.Domain.Common.Errors;
using ProCardsNew.Domain.DeckAggregate.ValueObjects;
using ProCardsNew.Domain.UserAggregate.ValueObjects;

namespace ProCardsNew.Application.Learning.Cards.Commands.GradeCard;

public class GradeCardCommandHandler
    : IRequestHandler<GradeCardCommand, ErrorOr<GradeCardCommandResult>>
{
    private readonly IUserRepository _userRepository;
    private readonly ICardRepository _cardRepository;
    private readonly IDeckRepository _deckRepository;

    public GradeCardCommandHandler(
        IUserRepository userRepository,
        ICardRepository cardRepository,
        IDeckRepository deckRepository)
    {
        _userRepository = userRepository;
        _cardRepository = cardRepository;
        _deckRepository = deckRepository;
    }

    public async Task<ErrorOr<GradeCardCommandResult>> Handle(
        GradeCardCommand command,
        CancellationToken cancellationToken)
    {
        if (await _userRepository.GetByIdAsync(UserId.Create(command.UserId)) is not { } user)
            return Errors.User.NotFound;

        if (await _deckRepository.GetByIdIncludingAsync(DeckId.Create(command.DeckId)) is not { } deck)
            return Errors.Deck.NotFound;

        if (await _cardRepository.GetByIdAsync(CardId.Create(command.CardId)) is not { } card)
            return Errors.Card.NotFound;

        if (!await _deckRepository.HasCard(deck.Id, card.Id))
            return Errors.User.AccessDenied;

        if (!await _deckRepository.HasAccess(deck.Id, user.Id))
            return Errors.User.AccessDenied;

        card.GradeCard(user.Id, deck.Id, command.Grade, command.TimeInSeconds);
        await _cardRepository.SaveChangesAsync();

        return new GradeCardCommandResult();
    }
}