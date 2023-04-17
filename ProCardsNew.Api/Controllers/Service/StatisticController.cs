using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProCardsNew.Api.Controllers.Common;
using ProCardsNew.Application.Service.Statistic.Queries.Statistic;
using ProCardsNew.Contracts.Service;

namespace ProCardsNew.Api.Controllers.Service;

[Route("statistic")]
[AllowAnonymous]
public class StatisticController: ApiController
{
    private readonly ISender _mediator;
    private readonly IMapper _mapper;

    public StatisticController(
        ISender mediator,
        IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> Statistic()
    {
        var createResult = await _mediator.Send(new StatisticQuery());

        return createResult.Match(
            result => Ok(_mapper.Map<StatisticResponse>(result)),
            errors => Problem(errors));
    }
}