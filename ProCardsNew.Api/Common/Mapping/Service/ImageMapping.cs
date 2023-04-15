using Mapster;
using ProCardsNew.Application.Editing.Cards.Commands.DeleteCard;
using ProCardsNew.Application.Service.Images.Commands.AddCardImage;
using ProCardsNew.Application.Service.Images.Commands.DeleteCardImage;
using ProCardsNew.Application.Service.Images.Queries.CardImage;
using ProCardsNew.Contracts.Common;
using ProCardsNew.Contracts.Service;

namespace ProCardsNew.Api.Common.Mapping.Service;

public class ImageMapping: IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<AddCardImageCommandResult, ResultResponse>();

        config.NewConfig<DeleteCardImageRequest, DeleteCardCommand>();
        config.NewConfig<DeleteCardImageCommandResult, ResultResponse>();

        config.NewConfig<CardImageRequest, CardImageQuery>();
        config.NewConfig<CardImageQueryResult, CardImageResponse>();
    }
}