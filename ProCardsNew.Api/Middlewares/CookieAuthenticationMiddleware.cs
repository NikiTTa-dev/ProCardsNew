using Microsoft.Extensions.Options;
using ProCardsNew.Infrastructure.Authentication;

namespace ProCardsNew.Api.Middlewares;

public class CookieAuthenticationMiddleware
{
    private readonly RequestDelegate _next;

    public CookieAuthenticationMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    // IMessageWriter is injected into InvokeAsync
    public async Task InvokeAsync(HttpContext context, IOptions<JwtSettings> jwtSettings)
    {
        var token = context.Request.Cookies[jwtSettings.Value.CookieName];
        if (!string.IsNullOrEmpty(token))
            context.Request.Headers.Add("Authorization", "Bearer " + token);
        
        await _next(context);
    }
}

public static class CookieAuthenticationMiddlewareExtensions
{
    public static IApplicationBuilder UseCookieAuthentication(
        this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<CookieAuthenticationMiddleware>();
    }
}