using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProCardsNew.Api.Controllers.Common;
using ProCardsNew.Application.Account.Profile.Queries.UserProfilePreview;
using ProCardsNew.Contracts.Account.Profile;

namespace ProCardsNew.Api.Controllers.Account;

[Route("users")]
public class ProfileController: ApiController
{
    private readonly ISender _mediator;
    private readonly IMapper _mapper;

    public ProfileController(
        ISender mediator,
        IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> UserProfilePreview([FromQuery] UserProfilePreviewRequest request)
    {
        var query = _mapper.Map<UserProfilePreviewQuery>(request);
        var queryResult = await _mediator.Send(query);

        return queryResult.Match(
            result => Ok(_mapper.Map<UserProfilePreviewResponse>(result)),
            errors => Problem(errors));
    }
}