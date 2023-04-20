using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProCardsNew.Api.Controllers.Common;
using ProCardsNew.Application.Learning.Cards.Commands.GradeCard;
using ProCardsNew.Application.Learning.Cards.Queries.StudyCards;
using ProCardsNew.Contracts.Common;
using ProCardsNew.Contracts.Learning;

namespace ProCardsNew.Api.Controllers.Learning;

[Route("cards")]
public class CardLearningController : ApiController
{
    private readonly ISender _mediator;
    private readonly IMapper _mapper;

    public CardLearningController(
        ISender mediator,
        IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }
    
    [HttpPost("grade")]
    public async Task<IActionResult> GradeCard(GradeCardRequest request)
    {
        if (request.UserId.ToString() != ClaimUserId)
            return AccessDenied;
        var command = _mapper.Map<GradeCardCommand>(request);
        var createResult = await _mediator.Send(command);

        return createResult.Match(
            result => Ok(_mapper.Map<ResultResponse>(result)),
            errors => Problem(errors));
    }

    [HttpGet]
    public async Task<IActionResult> StudyCards([FromQuery] StudyCardsRequest request)
    {
        if (request.UserId.ToString() != ClaimUserId)
            return AccessDenied;
        var query = _mapper.Map<StudyCardsQuery>(request);
        var createResult = await _mediator.Send(query);

        return createResult.Match(
            result => Ok(_mapper.Map<StudyCardsResponse>(result)),
            errors => Problem(errors));
    }
}