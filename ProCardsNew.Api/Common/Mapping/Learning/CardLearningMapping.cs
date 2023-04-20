using Mapster;
using ProCardsNew.Application.Learning.Cards.Commands.GradeCard;
using ProCardsNew.Application.Learning.Cards.Queries.StudyCards;
using ProCardsNew.Contracts.Common;
using ProCardsNew.Contracts.Learning;

namespace ProCardsNew.Api.Common.Mapping.Learning;

public class CardLearningMapping : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<GradeCardRequest, GradeCardCommand>();
        config.NewConfig<GradeCardCommandResult, ResultResponse>();

        config.NewConfig<StudyCardsRequest, StudyCardsQuery>();
        config.NewConfig<StudyCardsQueryResult, StudyCardsResponse>();
    }
}