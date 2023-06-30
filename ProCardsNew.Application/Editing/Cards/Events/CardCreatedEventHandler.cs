using MediatR;
using ProCardsNew.Application.Common.Interfaces.Persistence;
using ProCardsNew.Domain.CardAggregate.Events;

namespace ProCardsNew.Application.Editing.Cards.Events;

public class CardCreatedEventHandler : INotificationHandler<CardCreated>
{
    private readonly IStatisticRepository _userStatisticRepository;

    public CardCreatedEventHandler(
        IStatisticRepository userStatisticRepository)
    {
        _userStatisticRepository = userStatisticRepository;
    }

    public async Task Handle(CardCreated cardCreatedNotification, CancellationToken cancellationToken)
    {
        if (await _userStatisticRepository.GetUserStatistic(cardCreatedNotification.UserId) is { } statistic)
            statistic.IncreaseCardsCreated();
    }
}