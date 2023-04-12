using Mapster;
using ProCardsNew.Application.Editing.Decks.Commands.CreateDeck;
using ProCardsNew.Application.Editing.Decks.Commands.EditDeck;
using ProCardsNew.Application.Editing.Decks.Queries.UserDecksToEdit;
using ProCardsNew.Contracts.Common;
using ProCardsNew.Contracts.Editing.Decks;

namespace ProCardsNew.Api.Common.Mapping;

public class EditingMapping : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<CreateDeckRequest, CreateDeckCommand>();
        config.NewConfig<CreateDeckCommandResult, CreateDeckResponse>();

        config.NewConfig<UserDecksToEditRequest, UserDecksToEditQuery>();
        config.NewConfig<UserDecksToEditQueryResult, UserDecksToEditResponse>();
        config.NewConfig<DeckPreviewResponse, DeckPreview>()
            .TwoWays();

        config.NewConfig<EditDeckRequest, EditDeckCommand>();
        config.NewConfig<EditDeckResult, ResultResponse>();
    }
}