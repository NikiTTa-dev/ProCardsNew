using Mapster;
using ProCardsNew.Application.Editing.Decks.Commands.CreateDeck;
using ProCardsNew.Contracts.Editing.Decks;

namespace ProCardsNew.Api.Common.Mapping;

public class EditingMapping : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<CreateDeckRequest, CreateDeckCommand>();
        config.NewConfig<CreateDeckCommandResult, CreateDeckResponse>();
    }
}