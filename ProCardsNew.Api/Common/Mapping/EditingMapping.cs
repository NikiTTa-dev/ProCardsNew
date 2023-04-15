using Mapster;
using ProCardsNew.Application.Editing.Cards.Commands.CreateCard;
using ProCardsNew.Application.Editing.Cards.Queries.DeckCards;
using ProCardsNew.Application.Editing.Decks.Commands.CreateDeck;
using ProCardsNew.Application.Editing.Decks.Commands.DeleteDeck;
using ProCardsNew.Application.Editing.Decks.Commands.EditDeck;
using ProCardsNew.Application.Editing.Decks.Commands.EditDeckPassword;
using ProCardsNew.Application.Editing.Decks.Queries.UserDecksToEdit;
using ProCardsNew.Contracts.Common;
using ProCardsNew.Contracts.Editing.Cards;
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

        config.NewConfig<EditDeckPasswordRequest, EditDeckPasswordCommand>();
        config.NewConfig<EditDeckPasswordCommandResult, ResultResponse>();

        config.NewConfig<DeleteDeckRequest, DeleteDeckCommand>();
        config.NewConfig<DeleteDeckCommandResult, ResultResponse>();
        
        RegisterCards(config);
    }

    public void RegisterCards(TypeAdapterConfig config)
    {
        config.NewConfig<DeckCardsRequest, DeckCardsQuery>();
        config.NewConfig<DeckCardsQueryResult, DeckCardsResponse>();

        config.NewConfig<CardResult, CardResponse>()
            .TwoWays();

        config.NewConfig<CreateCardRequest, CreateCardCommand>();
        config.NewConfig<CreateCardCommandResult, CreateCardResponse>();
    }
}