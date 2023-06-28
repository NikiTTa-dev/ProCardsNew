using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProCardsNew.Api.Controllers.Common;
using ProCardsNew.Application.Editing.Cards.Commands.CreateCard;
using ProCardsNew.Application.Editing.Cards.Commands.DeleteCard;
using ProCardsNew.Application.Editing.Cards.Commands.EditCard;
using ProCardsNew.Application.Editing.Cards.Queries.DeckCards;
using ProCardsNew.Application.Editing.Cards.Queries.UserCards;
using ProCardsNew.Contracts.Common;
using ProCardsNew.Contracts.Editing.Cards;

namespace ProCardsNew.Api.Controllers.Editing;

[Route("editing/cards")]
public class CardController: ApiController
{
    private readonly ISender _mediator;
    private readonly IMapper _mapper;

    public CardController(
        ISender mediator,
        IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpPost]
    public async Task<IActionResult> CreateCard(CreateCardRequest request)
    {
        if (request.UserId.ToString() != ClaimUserId)
            return AccessDenied;
        var command = _mapper.Map<CreateCardCommand>(request);
        var createResult = await _mediator.Send(command);

        return createResult.Match(
            result => Ok(_mapper.Map<CreateCardResponse>(result)),
            errors => Problem(errors));
    }
    
    [HttpGet("fromdeck")]
    public async Task<IActionResult> DeckCards([FromQuery]DeckCardsRequest request)
    {
        if (request.UserId.ToString() != ClaimUserId)
            return AccessDenied;
        var query = _mapper.Map<DeckCardsQuery>(request);
        var createResult = await _mediator.Send(query);

        return createResult.Match(
            result => Ok(_mapper.Map<DeckCardsResponse>(result)),
            errors => Problem(errors));
    }

    [HttpGet]
    public async Task<IActionResult> UserCards([FromQuery] UserCardsRequest request)
    {
        if (request.UserId.ToString() != ClaimUserId)
            return AccessDenied;

        var query = _mapper.Map<UserCardsQuery>(request);
        var userCardsResult = await _mediator.Send(query);

        return userCardsResult.Match(
            result => Ok(_mapper.Map<UserCardsResponse>(result)),
            errors => Problem(errors));
    }

    [HttpPatch]
    public async Task<IActionResult> EditCard(EditCardRequest request)
    {
        if (request.UserId.ToString() != ClaimUserId)
            return AccessDenied;
        var command = _mapper.Map<EditCardCommand>(request);
        var createResult = await _mediator.Send(command);

        return createResult.Match(
            result => Ok(_mapper.Map<ResultResponse>(result)),
            errors => Problem(errors));
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteCard(DeleteCardRequest request)
    {
        if (request.UserId.ToString() != ClaimUserId)
            return AccessDenied;
        var command = _mapper.Map<DeleteCardCommand>(request);
        var createResult = await _mediator.Send(command);

        return createResult.Match(
            result => Ok(_mapper.Map<ResultResponse>(result)),
            errors => Problem(errors));
    }
}