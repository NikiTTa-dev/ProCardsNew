using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProCardsNew.Api.Controllers.Common;
using ProCardsNew.Application.Editing.Decks.Commands.CreateDeck;
using ProCardsNew.Application.Editing.Decks.Commands.EditDeck;
using ProCardsNew.Application.Editing.Decks.Queries.UserDecksToEdit;
using ProCardsNew.Contracts.Editing.Decks;

namespace ProCardsNew.Api.Controllers.Editing;

[Route("editing/decks")]
public class DeckController: ApiController
{
    private readonly ISender _mediator;
    private readonly IMapper _mapper;

    public DeckController(
        ISender mediator,
        IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpPost]
    public async Task<IActionResult> CreateDeck(CreateDeckRequest request)
    {
        if (request.UserId.ToString() != ClaimUserId)
            return AccessDenied;
        var command = _mapper.Map<CreateDeckCommand>(request);
        var createResult = await _mediator.Send(command);

        return createResult.Match(
            result => Ok(_mapper.Map<CreateDeckResponse>(result)),
            errors => Problem(errors));
    }

    [HttpPost("get")]
    public async Task<IActionResult> UserDecksToEdit(UserDecksToEditRequest request)
    {
        if (request.UserId.ToString() != ClaimUserId)
            return AccessDenied;
        var query = _mapper.Map<UserDecksToEditQuery>(request);
        var createResult = await _mediator.Send(query);

        return createResult.Match(
            result => Ok(_mapper.Map<UserDecksToEditResponse>(result)),
            errors => Problem(errors));
    }
    
    [HttpPatch]
    public async Task<IActionResult> EditDeck(EditDeckRequest request)
    {
        if (request.UserId.ToString() != ClaimUserId)
            return AccessDenied;
        var query = _mapper.Map<EditDeckCommand>(request);
        var createResult = await _mediator.Send(query);

        return createResult.Match(
            result => Ok(_mapper.Map<EditDeckResult>(result)),
            errors => Problem(errors));
    }
}