using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProCardsNew.Api.Controllers.Common;
using ProCardsNew.Application.Learning.Decks.Commands.AddDeck;
using ProCardsNew.Application.Learning.Decks.Commands.Deck;
using ProCardsNew.Application.Learning.Decks.Commands.RemoveDeckFromLatest;
using ProCardsNew.Application.Learning.Decks.Queries.UserDecks;
using ProCardsNew.Contracts.Common;
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
    public async Task<IActionResult> AddDeck(AddDeckRequest request)
    {
        if (request.UserId.ToString() != ClaimUserId)
            return AccessDenied;
        var command = _mapper.Map<AddDeckCommand>(request);
        var createResult = await _mediator.Send(command);

        return createResult.Match(
            result => Ok(_mapper.Map<DeckResponse>(result)),
            errors => Problem(errors));
    }

    [HttpGet]
    public async Task<IActionResult> UserDecks([FromQuery] UserDecksRequest request)
    {
        if (request.UserId.ToString() != ClaimUserId)
            return AccessDenied;
        var query = _mapper.Map<UserDecksQuery>(request);
        var createResult = await _mediator.Send(query);

        return createResult.Match(
            result => Ok(_mapper.Map<UserDecksResponse>(result)),
            errors => Problem(errors));
    }
    
    [HttpGet("deck")]
    public async Task<IActionResult> Deck([FromQuery] DeckRequest request)
    {
        if (request.UserId.ToString() != ClaimUserId)
            return AccessDenied;
        var query = _mapper.Map<DeckCommand>(request);
        var createResult = await _mediator.Send(query);

        return createResult.Match(
            result => Ok(_mapper.Map<DeckResponse>(result)),
            errors => Problem(errors));
    }
    
    [HttpDelete("remove")]
    public async Task<IActionResult> RemoveDeckFromLatest(RemoveDeckFromLatestRequest request)
    {
        if (request.UserId.ToString() != ClaimUserId)
            return AccessDenied;
        var query = _mapper.Map<RemoveDeckFromLatestCommand>(request);
        var createResult = await _mediator.Send(query);

        return createResult.Match(
            result => Ok(_mapper.Map<ResultResponse>(result)),
            errors => Problem(errors));
    }
}