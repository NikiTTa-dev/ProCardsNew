using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProCardsNew.Api.Controllers.Common;
using ProCardsNew.Application.Editing.Decks.Commands.AddCard;
using ProCardsNew.Application.Editing.Decks.Commands.CreateDeck;
using ProCardsNew.Application.Editing.Decks.Commands.DeleteDeck;
using ProCardsNew.Application.Editing.Decks.Commands.EditDeck;
using ProCardsNew.Application.Editing.Decks.Commands.EditDeckPassword;
using ProCardsNew.Application.Editing.Decks.Commands.RemoveCardFromDeck;
using ProCardsNew.Application.Editing.Decks.Queries.UserDecksToEdit;
using ProCardsNew.Contracts.Common;
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
    
    [HttpPost("addcard")]
    public async Task<IActionResult> AddCard(AddCardRequest request)
    {
        if (request.UserId.ToString() != ClaimUserId)
            return AccessDenied;
        var command = _mapper.Map<AddCardCommand>(request);
        var addCardResult = await _mediator.Send(command);

        return addCardResult.Match(
            result => Ok(_mapper.Map<AddCardResponse>(result)),
            errors => Problem(errors));
    }

    [HttpGet]
    public async Task<IActionResult> UserDecksToEdit([FromQuery]UserDecksToEditRequest request)
    {
        if (request.UserId.ToString() != ClaimUserId)
            return AccessDenied;
        var query = _mapper.Map<UserDecksToEditQuery>(request);
        var userDecksResult = await _mediator.Send(query);

        return userDecksResult.Match(
            result => Ok(_mapper.Map<UserDecksToEditResponse>(result)),
            errors => Problem(errors));
    }
    
    [HttpPatch]
    public async Task<IActionResult> EditDeck(EditDeckRequest request)
    {
        if (request.UserId.ToString() != ClaimUserId)
            return AccessDenied;
        var command = _mapper.Map<EditDeckCommand>(request);
        var editDeckResult = await _mediator.Send(command);

        return editDeckResult.Match(
            result => Ok(_mapper.Map<ResultResponse>(result)),
            errors => Problem(errors));
    }

    [HttpPatch("password")]
    public async Task<IActionResult> EditDeckPassword(EditDeckPasswordRequest request)
    {
        if (request.UserId.ToString() != ClaimUserId)
            return AccessDenied;
        var command = _mapper.Map<EditDeckPasswordCommand>(request);
        var editDeckPasswordResult = await _mediator.Send(command);

        return editDeckPasswordResult.Match(
            result => Ok(_mapper.Map<ResultResponse>(result)),
            errors => Problem(errors));
    }
    
    [HttpDelete]
    public async Task<IActionResult> DeleteDeck(DeleteDeckRequest request)
    {
        if (request.UserId.ToString() != ClaimUserId)
            return AccessDenied;
        var command = _mapper.Map<DeleteDeckCommand>(request);
        var deleteDeckResult = await _mediator.Send(command);

        return deleteDeckResult.Match(
            result => Ok(_mapper.Map<ResultResponse>(result)),
            errors => Problem(errors));
    }
    
    [HttpDelete("removecard")]
    public async Task<IActionResult> RemoveCardFromDeck(RemoveCardFromDeckRequest request)
    {
        if (request.UserId.ToString() != ClaimUserId)
            return AccessDenied;
        var command = _mapper.Map<RemoveCardFromDeckCommand>(request);
        var removeCardFromDeckResult = await _mediator.Send(command);

        return removeCardFromDeckResult.Match(
            result => Ok(_mapper.Map<RemoveCardFromDeckResponse>(result)),
            errors => Problem(errors));
    }
}