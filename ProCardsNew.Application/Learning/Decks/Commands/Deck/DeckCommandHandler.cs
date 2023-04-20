using ErrorOr;
using MediatR;
using ProCardsNew.Application.Common.Interfaces.Persistence;
using ProCardsNew.Application.Learning.Decks.Common;
using ProCardsNew.Domain.Common.Errors;
using ProCardsNew.Domain.DeckAggregate.ValueObjects;
using ProCardsNew.Domain.UserAggregate.ValueObjects;

namespace ProCardsNew.Application.Learning.Decks.Commands.Deck;

public class DeckCommandHandler
    : IRequestHandler<DeckCommand, ErrorOr<DeckResult>>
{
    private readonly IUserRepository _userRepository;
    private readonly IDeckRepository _deckRepository;
    private readonly IStatisticRepository _statisticRepository;

    public DeckCommandHandler(
        IUserRepository userRepository,
        IDeckRepository deckRepository,
        IStatisticRepository statisticRepository)
    {
        _userRepository = userRepository;
        _deckRepository = deckRepository;
        _statisticRepository = statisticRepository;
    }
    
    public async Task<ErrorOr<DeckResult>> Handle(DeckCommand command, CancellationToken cancellationToken)
    {
        if (await _userRepository.GetByIdAsync(UserId.Create(command.UserId)) is not { } user)
            return Errors.User.NotFound;

        if (await _deckRepository.GetByIdIncludingAsync(
                DeckId.Create(command.DeckId),
                d => d.Owner!) is not { } deck)
            return Errors.Deck.NotFound;

        if (await _deckRepository.GetUserDeck(deck.Id, user.Id) is not { } userDeck)
            return Errors.User.AccessDenied;
        
        userDeck.UpdateOpenedAtDateTime();
        await _deckRepository.SaveChangesAsync();
        
        var statistic =
            await _statisticRepository.GetDeckStatisticsWhereIncludingAsync(
                deck.Id,
                ds => ds.User!);

        var cardsCount = await _deckRepository.GetCardsCount(deck.Id);
        
        return new DeckResult(
            Id: deck.Id.Value,
            DeckName: deck.Name,
            Description: deck.Description,
            OwnerId: deck.OwnerId.Value,
            OwnerLogin: deck.Owner!.Login,
            CardsCount: cardsCount,
            statistic.ConvertAll(s =>
                new DeckStatisticResult(
                    s.UserId.Value,
                    Login: s.User!.Login,
                    s.Score)));
    }
}