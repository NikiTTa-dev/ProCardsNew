using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Identity;
using ProCardsNew.Application.Common.Interfaces.Authentication;
using ProCardsNew.Application.Common.Interfaces.Persistence;
using ProCardsNew.Application.Learning.Decks.Common;
using ProCardsNew.Domain.Common.Errors;
using ProCardsNew.Domain.DeckAggregate.ValueObjects;
using ProCardsNew.Domain.UserAggregate.ValueObjects;

namespace ProCardsNew.Application.Learning.Decks.Commands.AddDeck;

public class AddDeckCommandHandler
    : IRequestHandler<AddDeckCommand, ErrorOr<DeckResult>>
{
    private readonly IUserRepository _userRepository;
    private readonly IDeckRepository _deckRepository;
    private readonly IStatisticRepository _statisticRepository;
    private readonly IPasswordHasherService _passwordHasherService;

    public AddDeckCommandHandler(
        IUserRepository userRepository,
        IDeckRepository deckRepository,
        IStatisticRepository statisticRepository,
        IPasswordHasherService passwordHasherService)
    {
        _userRepository = userRepository;
        _deckRepository = deckRepository;
        _statisticRepository = statisticRepository;
        _passwordHasherService = passwordHasherService;
    }

    public async Task<ErrorOr<DeckResult>> Handle(AddDeckCommand command, CancellationToken cancellationToken)
    {
        if (await _userRepository.GetByIdAsync(UserId.Create(command.UserId)) is not { } user)
            return Errors.User.NotFound;

        if (await _deckRepository.GetByIdIncludingAsync(
                DeckId.Create(command.DeckId),
                d => d.Owner!) is not { } deck)
            return Errors.Deck.NotFound;

        if (await _deckRepository.GetAccessibleDeckAccessAsync(deck.Id) is not { } deckAccess)
            return Errors.User.AccessDenied;

        if (await _deckRepository.HasAccess(deck.Id, user.Id) || deck.OwnerId == user.Id)
            return Errors.Deck.Duplicate;

        var verificationResult = _passwordHasherService
            .VerifyPasswordHash(deck.PasswordHash!, command.Password);

        if (verificationResult
            is PasswordVerificationResult.Failed)
            return Errors.Deck.InvalidCredentials;

        if (verificationResult
            is PasswordVerificationResult.SuccessRehashNeeded)
            deck.EditPassword(_passwordHasherService.GeneratePasswordHash(command.Password));

        deckAccess.AddUser(user.Id);
        if (!await _statisticRepository.HasStatistic(deck.Id, user.Id))
            deck.AddStatistic(user.Id);
        await _deckRepository.SaveChangesAsync();

        var statistic =
            await _statisticRepository.GetDeckStatisticsWhereIncludingAsync(
                deck.Id,
                ds => ds.User!);

        var cardsCount = await _deckRepository.GetCardsCount(deck.Id);
        return new DeckResult(
            Id: deck.Id.Value,
            Name: deck.Name,
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