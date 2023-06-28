using ErrorOr;
using MediatR;
using ProCardsNew.Application.Common.Interfaces.Persistence;
using ProCardsNew.Application.Editing.Cards.Queries.DeckCards;
using ProCardsNew.Domain.CardAggregate;
using ProCardsNew.Domain.Common.Errors;
using ProCardsNew.Domain.DeckAggregate.ValueObjects;
using ProCardsNew.Domain.UserAggregate.ValueObjects;

namespace ProCardsNew.Application.Learning.Cards.Queries.StudyCards;

public class StudyCardsQueryHandler
    : IRequestHandler<StudyCardsQuery, ErrorOr<StudyCardsQueryResult>>
{
    private readonly IUserRepository _userRepository;
    private readonly ICardRepository _cardRepository;
    private readonly IDeckRepository _deckRepository;

    public StudyCardsQueryHandler(
        IUserRepository userRepository,
        ICardRepository cardRepository,
        IDeckRepository deckRepository)
    {
        _userRepository = userRepository;
        _cardRepository = cardRepository;
        _deckRepository = deckRepository;
    }

    public async Task<ErrorOr<StudyCardsQueryResult>> Handle(StudyCardsQuery query, CancellationToken cancellationToken)
    {
        if (await _userRepository.GetByIdIncludeAsync(
                UserId.Create(query.UserId)) is not { } user)
            return Errors.User.NotFound;

        if (await _deckRepository.GetByIdAsync(DeckId.Create(query.DeckId)) is not { } deck)
            return Errors.Deck.NotFound;

        if (!await _deckRepository.HasAccess(deck.Id, user.Id))
            return Errors.User.AccessDenied;

        var cards = await _cardRepository.GetCardsWithGradesAsync(deck.Id, user.Id);
        List<(float, Card)> cardWeights = new();
        foreach (var card in cards)
        {
            var weight = 0f;
            foreach (var grade in card.Grades)
            {
                weight += 1f / (1f + (float)(DateTime.UtcNow - grade.GradedAtDateTime).TotalMinutes) * grade.GradeValue;
            }

            cardWeights.Add((weight, card));
        }

        return new StudyCardsQueryResult(
            DeckName: deck.Name,
            Cards: cardWeights
                .OrderBy(cw => cw.Item1)
                .Take(5)
                .Select(cw => new CardResult(
                    Id: cw.Item2.Id.Value,
                    FrontSide: cw.Item2.FrontSide,
                    BackSide: cw.Item2.BackSide,
                    HasFrontImage: cw.Item2.FrontImage != null,
                    HasBackImage: cw.Item2.BackImage != null))
                .ToList());
    }
}