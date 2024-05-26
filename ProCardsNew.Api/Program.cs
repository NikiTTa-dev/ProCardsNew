using Microsoft.AspNetCore.CookiePolicy;
using ProCardsNew.Api;
using ProCardsNew.Api.Middlewares;
using ProCardsNew.Application;
using ProCardsNew.Infrastructure;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((_, configuration) =>
{
    configuration.ReadFrom.Configuration(builder.Configuration);
});

builder.Services
    .AddApi()
    .AddApplication()
    .AddInfrastructure(builder.Configuration);

var app = builder.Build();

app.UseHttpsRedirection();

app.UseCookieReader();

app.UseExceptionHandler("/error");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseCors(c =>
    {
        c.AllowCredentials();
        c.WithOrigins(
            "https://localhost:3000",
            "http://localhost:3000");
        c.AllowAnyMethod();
        c.AllowAnyHeader();
    });
    app.UseCookiePolicy(new CookiePolicyOptions
    {
        Secure = CookieSecurePolicy.SameAsRequest,
        MinimumSameSitePolicy = SameSiteMode.None,
        HttpOnly = HttpOnlyPolicy.Always
    });
}
else
{
    app.UseCors(c =>
    {
        c.AllowCredentials();
        c.WithOrigins(
            "https://localhost:3000",
            "http://localhost:3000");
        c.AllowAnyMethod();
        c.AllowAnyHeader();
    });
    
    app.UseCookiePolicy(new CookiePolicyOptions
    {
        Secure = CookieSecurePolicy.Always,
        MinimumSameSitePolicy = SameSiteMode.Strict,
        HttpOnly = HttpOnlyPolicy.Always
    });
}

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();