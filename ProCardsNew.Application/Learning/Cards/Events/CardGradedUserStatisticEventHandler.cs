using MediatR;
using ProCardsNew.Application.Common.Interfaces.Persistence;
using ProCardsNew.Domain.CardAggregate.Events;

namespace ProCardsNew.Application.Learning.Cards.Events;

public class CardGradedUserStatisticEventHandler
    : INotificationHandler<CardGraded>
{
    private readonly IStatisticRepository _statisticRepository;

    public CardGradedUserStatisticEventHandler(
        IStatisticRepository statisticRepository)
    {
        _statisticRepository = statisticRepository;
    }
    
    public async Task Handle(CardGraded notification, CancellationToken cancellationToken)
    {
        if (await _statisticRepository.GetUserStatistic(notification.UserId) is { } statistic)
            statistic.IncreaseStatistic(1, notification.Hours);
    }
}