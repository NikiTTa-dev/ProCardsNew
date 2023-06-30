using Mapster;
using ProCardsNew.Application.Editing.Decks.Commands.AddCard;
using ProCardsNew.Application.Editing.Decks.Commands.CreateDeck;
using ProCardsNew.Application.Editing.Decks.Commands.DeleteDeck;
using ProCardsNew.Application.Editing.Decks.Commands.EditDeck;
using ProCardsNew.Application.Editing.Decks.Commands.EditDeckPassword;
using ProCardsNew.Application.Editing.Decks.Commands.RemoveCardFromDeck;
using ProCardsNew.Application.Editing.Decks.Queries.UserDecksToEdit;
using ProCardsNew.Contracts.Common;
using ProCardsNew.Contracts.Editing.Decks;

namespace ProCardsNew.Api.Common.Mapping.Editing;

public class DeckEditingMapping : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<CreateDeckRequest, CreateDeckCommand>();
        config.NewConfig<CreateDeckCommandResult, CreateDeckResponse>();

        config.NewConfig<AddCardRequest, AddCardCommand>();
        config.NewConfig<AddCardCommandResult, AddCardResponse>();

        config.NewConfig<RemoveCardFromDeckRequest, RemoveCardFromDeckCommand>();
        config.NewConfig<RemoveCardFromDeckCommandResult, RemoveCardFromDeckResponse>();

        config.NewConfig<UserDecksToEditRequest, UserDecksToEditQuery>();
        config.NewConfig<UserDecksToEditQueryResult, UserDecksToEditResponse>();
        config.NewConfig<DeckPreviewResponse, DeckPreview>()
            .TwoWays();

        config.NewConfig<EditDeckRequest, EditDeckCommand>();
        config.NewConfig<EditDeckResult, ResultResponse>();

        config.NewConfig<EditDeckPasswordRequest, EditDeckPasswordCommand>();
        config.NewConfig<EditDeckPasswordCommandResult, ResultResponse>();

        config.NewConfig<DeleteDeckRequest, DeleteDeckCommand>();
        config.NewConfig<DeleteDeckCommandResult, ResultResponse>();
    }
}