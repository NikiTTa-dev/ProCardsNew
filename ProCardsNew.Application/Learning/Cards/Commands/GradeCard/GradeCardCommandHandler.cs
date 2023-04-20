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
    private readonly IStatisticRepository _statisticRepository;

    public GradeCardCommandHandler(
        IUserRepository userRepository,
        ICardRepository cardRepository,
        IDeckRepository deckRepository, 
        IStatisticRepository statisticRepository)
    {
        _userRepository = userRepository;
        _cardRepository = cardRepository;
        _deckRepository = deckRepository;
        _statisticRepository = statisticRepository;
    }
    
    public async Task<ErrorOr<GradeCardCommandResult>> Handle(GradeCardCommand command, CancellationToken cancellationToken)
    {
        if (await _userRepository.GetByIdIncludeAsync(
                UserId.Create(command.UserId),
                u => u.Statistic) is not { } user)
            return Errors.User.NotFound;

        if (await _deckRepository.GetByIdIncludingAsync(DeckId.Create(command.DeckId)) is not { } deck)
            return Errors.Deck.NotFound;

        if (await _cardRepository.GetByIdAsync(CardId.Create(command.CardId)) is not { } card)
            return Errors.Card.NotFound;

        if (!await _deckRepository.HasCard(deck.Id, card.Id))
            return Errors.User.AccessDenied;

        if (!await _deckRepository.HasAccess(deck.Id, user.Id))
            return Errors.User.AccessDenied;

        var hours = command.TimeInSeconds / 3600f;
        var statistic = (await _statisticRepository.GetDeckStatisticsAsync(deck.Id, user.Id))!;
        statistic.IncreaseStatistic(1, hours);
        user.Statistic!.IncreaseStatistic(1, hours);
        card.GradeCard(user.Id, deck.Id, command.Grade);
        await _cardRepository.SaveChangesAsync();

        return new GradeCardCommandResult();
    }
}