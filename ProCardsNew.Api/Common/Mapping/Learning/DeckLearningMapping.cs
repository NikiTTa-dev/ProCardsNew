using Mapster;
using ProCardsNew.Application.Learning.Decks.Commands.AddDeck;
using ProCardsNew.Application.Learning.Decks.Commands.Deck;
using ProCardsNew.Application.Learning.Decks.Commands.RemoveDeckFromLatest;
using ProCardsNew.Application.Learning.Decks.Common;
using ProCardsNew.Application.Learning.Decks.Queries.UserDecks;
using ProCardsNew.Contracts.Common;
using ProCardsNew.Contracts.Learning;

namespace ProCardsNew.Api.Common.Mapping.Learning;

public class DeckLearningMapping : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<AddDeckRequest, AddDeckCommand>();
        config.NewConfig<DeckResult, DeckResponse>();
        config.NewConfig<DeckStatisticResult, DeckStatisticResponse>();

        config.NewConfig<DeckRequest, DeckCommand>();
        
        config.NewConfig<UserDecksRequest, UserDecksQuery>();
        config.NewConfig<UserDecksQueryResult, UserDecksResponse>();

        config.NewConfig<RemoveDeckFromLatestRequest, RemoveDeckFromLatestCommand>();
        config.NewConfig<RemoveDeckFromLatestCommandResult, ResultResponse>();
    }
}