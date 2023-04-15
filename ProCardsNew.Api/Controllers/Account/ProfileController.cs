using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProCardsNew.Api.Controllers.Common;
using ProCardsNew.Application.Account.Profile.Commands.EditPassword;
using ProCardsNew.Application.Account.Profile.Commands.EditProfile;
using ProCardsNew.Application.Account.Profile.Queries.UserProfile;
using ProCardsNew.Application.Account.Profile.Queries.UserProfilePreview;
using ProCardsNew.Contracts.Account.Profile;
using ProCardsNew.Contracts.Common;

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

    [HttpGet("preview")]
    public async Task<IActionResult> UserProfilePreview([FromQuery] UserProfilePreviewRequest request)
    {
        var query = _mapper.Map<UserProfilePreviewQuery>(request);
        var queryResult = await _mediator.Send(query);

        return queryResult.Match(
            result => Ok(_mapper.Map<UserProfilePreviewResponse>(result)),
            errors => Problem(errors));
    }
    
    [HttpGet("profile")]
    public async Task<IActionResult> UserProfile([FromQuery] UserProfileRequest request)
    {
        if (request.UserId.ToString() != ClaimUserId)
            return AccessDenied;
        var query = _mapper.Map<UserProfileQuery>(request);
        var queryResult = await _mediator.Send(query);

        return queryResult.Match(
            result => Ok(_mapper.Map<UserProfileResponse>(result)),
            errors => Problem(errors));
    }
    
    [HttpPatch("profile")]
    public async Task<IActionResult> EditProfile(EditProfileRequest request)
    {
        if (request.UserId.ToString() != ClaimUserId)
            return AccessDenied;
        var command = _mapper.Map<EditProfileCommand>(request);
        var commandResult = await _mediator.Send(command);

        return commandResult.Match(
            result => Ok(_mapper.Map<ResultResponse>(result)),
            errors => Problem(errors));
    }
    
    [HttpPatch("password")]
    public async Task<IActionResult> EditPassword(EditPasswordRequest request)
    {
        if (request.UserId.ToString() != ClaimUserId)
            return AccessDenied;
        var command = _mapper.Map<EditPasswordCommand>(request);
        var commandResult = await _mediator.Send(command);

        return commandResult.Match(
            result => Ok(_mapper.Map<ResultResponse>(result)),
            errors => Problem(errors));
    }
}