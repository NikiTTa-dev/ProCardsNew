using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProCardsNew.Api.Controllers.Common;
using ProCardsNew.Application.Editing.Decks.Commands.CreateDeck;
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
}