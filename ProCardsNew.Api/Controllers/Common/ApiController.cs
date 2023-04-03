using System.Security.Claims;
using ErrorOr;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using ProCardsNew.Api.Common.Http;
using ProCardsNew.Api.Filters;
using ProCardsNew.Domain.Common.Errors;

namespace ProCardsNew.Api.Controllers.Common;

[ApiController]
[Authorize]
[ProCardsActionFilter]
public class ApiController: ControllerBase
{
    protected IActionResult AccessDenied => 
        Problem(new List<Error> { Errors.User.AccessDenied });

    protected string? ClaimUserId => User.Claims
        .FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?
        .Value;
    
    protected IActionResult Problem(List<Error> errors)
    {
        if (errors.Count is 0)
        {
            return Problem();
        }
        
        if (errors.All(error => error.Type == ErrorType.Validation))
        {
            return ValidationProblem(errors);
        }
        
        HttpContext.Items[HttpContextKeys.Errors] = errors;
        
        return Problem(errors[0]);
    }

    private IActionResult Problem(Error error)
    {
        var statusCode = error.Type switch
        {
            ErrorType.Conflict => StatusCodes.Status409Conflict,
            ErrorType.Failure => StatusCodes.Status403Forbidden,
            ErrorType.Validation => StatusCodes.Status400BadRequest,
            ErrorType.NotFound => StatusCodes.Status404NotFound,
            _ => StatusCodes.Status500InternalServerError
        };

        return Problem(statusCode: statusCode, title: error.Description);
    }

    private IActionResult ValidationProblem(List<Error> errors)
    {
        var modelStateDictionary = new ModelStateDictionary();

        foreach (var error in errors)
        {
            modelStateDictionary.AddModelError(
                error.Code,
                error.Description);
        }

        return ValidationProblem(modelStateDictionary);
    }
}