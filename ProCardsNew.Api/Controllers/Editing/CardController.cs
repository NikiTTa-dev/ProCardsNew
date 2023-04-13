using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProCardsNew.Api.Controllers.Common;
using ProCardsNew.Application.Editing.Cards.Queries.DeckCards;
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
    public async Task<IActionResult> DeckCards(DeckCardsRequest request)
    {
        if (request.UserId.ToString() != ClaimUserId)
            return AccessDenied;
        var query = _mapper.Map<DeckCardsQuery>(request);
        var createResult = await _mediator.Send(query);

        return createResult.Match(
            result => Ok(_mapper.Map<DeckCardsResponse>(result)),
            errors => Problem(errors));
    }
}