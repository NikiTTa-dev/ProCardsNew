using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace ProCardsNew.Api.Controllers.Common;

[ApiExplorerSettings(IgnoreApi = true)]
public class ErrorsController : ControllerBase
{
    [Route("/error")]
    public IActionResult Error()
    {
        if (HttpContext.Features.Get<IExceptionHandlerFeature>()?.Error is { } exception)
            return Problem(title: exception.Message);
        
        return Problem("Something went wrong.");
    }
}