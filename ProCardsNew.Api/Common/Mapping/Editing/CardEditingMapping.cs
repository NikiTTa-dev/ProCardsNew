using Mapster;
using ProCardsNew.Application.Editing.Cards.Commands.CreateCard;
using ProCardsNew.Application.Editing.Cards.Commands.DeleteCard;
using ProCardsNew.Application.Editing.Cards.Commands.EditCard;
using ProCardsNew.Application.Editing.Cards.Queries.DeckCards;
using ProCardsNew.Application.Editing.Cards.Queries.UserCards;
using ProCardsNew.Contracts.Common;
using ProCardsNew.Contracts.Editing.Cards;

namespace ProCardsNew.Api.Common.Mapping.Editing;

public class CardEditingMapping : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<DeckCardsRequest, DeckCardsQuery>();
        config.NewConfig<DeckCardsQueryResult, DeckCardsResponse>();

        config.NewConfig<UserCardsRequest, UserCardsQuery>();
        config.NewConfig<UserCardsQueryResult, UserCardsResponse>();

        config.NewConfig<CardResult, CardResponse>()
            .TwoWays();

        config.NewConfig<CreateCardRequest, CreateCardCommand>();
        config.NewConfig<CreateCardCommandResult, CreateCardResponse>();

        config.NewConfig<EditCardRequest, EditCardCommand>();
        config.NewConfig<EditCardCommandResult, ResultResponse>();

        config.NewConfig<DeleteCardRequest, DeleteCardCommand>();
        config.NewConfig<DeleteCardCommandResult, ResultResponse>();
    }
}