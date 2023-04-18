using Mapster;
using ProCardsNew.Application.Learning.Decks.Commands.AddDeck;
using ProCardsNew.Application.Learning.Decks.Common;
using ProCardsNew.Contracts.Learning;

namespace ProCardsNew.Api.Common.Mapping.Learning;

public class DeckLearningMapping : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<AddDeckRequest, AddDeckCommand>();
        config.NewConfig<DeckResult, AddDeckResponse>();
        config.NewConfig<DeckStatisticResult, DeckStatisticResponse>();
    }
}