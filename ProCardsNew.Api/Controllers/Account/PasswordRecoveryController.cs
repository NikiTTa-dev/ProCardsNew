using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProCardsNew.Api.Controllers.Common;
using ProCardsNew.Application.Account.PasswordRecovery.Commands.PasswordRecovery;
using ProCardsNew.Application.Account.PasswordRecovery.Commands.PasswordRecoveryNewPassword;
using ProCardsNew.Application.Account.PasswordRecovery.Queries.PasswordRecoveryCode;
using ProCardsNew.Contracts.Account.PasswordRecovery;

namespace ProCardsNew.Api.Controllers.Account;

[Route("account/recovery")]
[AllowAnonymous]
public class PasswordRecoveryController: ApiController
{
    private readonly ISender _mediator;
    private readonly IMapper _mapper;

    public PasswordRecoveryController(
        ISender mediator,
        IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }
    
    [HttpPost]
    public async Task<IActionResult> SendEmailAsync(
        PasswordRecoveryRequest request)
    {
        var command = _mapper.Map<PasswordRecoveryCommand>(request);
        var passwordRecoveryResult = await _mediator.Send(command);
        
        return passwordRecoveryResult.Match(
            result => Ok(_mapper.Map<PasswordRecoveryResponse>(result)),
            errors => Problem(errors));
    }

    [HttpPost("code")]
    public async Task<IActionResult> ValidateCodeAsync(
        PasswordRecoveryCodeRequest request)
    {
        var query = _mapper.Map<PasswordRecoveryCodeQuery>(request);
        var passwordRecoveryResult = await _mediator.Send(query);

        return passwordRecoveryResult.Match(
            result => Ok(_mapper.Map<PasswordRecoveryResponse>(result)),
            errors => Problem(errors));
    }

    [HttpPost("newpass")]
    public async Task<IActionResult> ChangePasswordAsync(
        PasswordRecoveryNewPasswordRequest request)
    {
        var command = _mapper.Map<PasswordRecoveryNewPasswordCommand>(request);
        var passwordRecoveryResult = await _mediator.Send(command);

        return passwordRecoveryResult.Match(
            result => Ok(_mapper.Map<PasswordRecoveryResponse>(result)),
            errors => Problem(errors));
    }
}