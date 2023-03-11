using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using ProCardsNew.Api.Controllers.Common;
using ProCardsNew.Application.Account.Authentication.Commands.Register;
using ProCardsNew.Application.Account.Authentication.Queries.Login;
using ProCardsNew.Contracts.Account.Authentication;
using ProCardsNew.Infrastructure.Authentication;

namespace ProCardsNew.Api.Controllers.Account;

[Route("account")]
[AllowAnonymous]
public class AuthenticationController : ApiController
{
    private readonly ISender _mediator;
    private readonly IMapper _mapper;
    private readonly JwtSettings _jwtSettings;

    public AuthenticationController(
        ISender mediator,
        IMapper mapper,
        IOptions<JwtSettings> jwtSettings)
    {
        _mediator = mediator;
        _mapper = mapper;
        _jwtSettings = jwtSettings.Value;
    }


    [HttpPost("register")]
    public async Task<IActionResult> RegisterAsync(RegisterRequest request)
    {
        var command = _mapper.Map<RegisterCommand>(request);

        var registerResult = await _mediator.Send(command);

        return registerResult.Match(
            result => Authenticate(_mapper.Map<AuthenticationResponse>(result)),
            errors => Problem(errors));
    }

    [HttpPost("login")]
    public async Task<IActionResult> LoginAsync(LoginRequest request)
    {
        var query = _mapper.Map<LoginQuery>(request);
        var authResult = await _mediator.Send(query);

        return authResult.Match(
            result => Authenticate(_mapper.Map<AuthenticationResponse>(result)),
            errors => Problem(errors));
    }

    private IActionResult Authenticate(AuthenticationResponse result)
    {
        HttpContext.Response.Cookies.Append(_jwtSettings.CookieName, result.Token,
            new CookieOptions
            {
                MaxAge = TimeSpan.FromMinutes(_jwtSettings.ExpiryMinutes)
            });
        return Ok(result);
    }
}