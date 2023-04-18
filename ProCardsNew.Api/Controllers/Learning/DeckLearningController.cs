using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProCardsNew.Api.Controllers.Common;
using ProCardsNew.Application.Learning.Decks.Commands.AddDeck;
using ProCardsNew.Contracts.Learning;

namespace ProCardsNew.Api.Controllers.Learning;

[Route("decks")]
public class DeckLearningController : ApiController
{
    private readonly ISender _mediator;
    private readonly IMapper _mapper;

    public DeckLearningController(
        ISender mediator,
        IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpPost("add")]
    public async Task<IActionResult> CreateDeck(AddDeckRequest request)
    {
        if (request.UserId.ToString() != ClaimUserId)
            return AccessDenied;
        var command = _mapper.Map<AddDeckCommand>(request);
        var createResult = await _mediator.Send(command);

        return createResult.Match(
            result => Ok(_mapper.Map<AddDeckResponse>(result)),
            errors => Problem(errors));
    }
}