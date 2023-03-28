using Microsoft.AspNetCore.CookiePolicy;
using ProCardsNew.Api;
using ProCardsNew.Api.Middlewares;
using ProCardsNew.Application;
using ProCardsNew.Domain.CardAggregate;
using ProCardsNew.Domain.DeckAggregate;
using ProCardsNew.Domain.UserAggregate;
using ProCardsNew.Infrastructure;
using ProCardsNew.Infrastructure.Persistence;
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

app.UseExceptionHandler("/error");

app.UseHttpsRedirection();

app.UseCookiePolicy(new CookiePolicyOptions
{
    MinimumSameSitePolicy = SameSiteMode.Strict,
    HttpOnly = HttpOnlyPolicy.Always,
    Secure = CookieSecurePolicy.Always
});

app.Map("mock",async context =>
{
    var dbContext = context.RequestServices.GetService<ProCardsDbContext>()!;

    var user = User.Create("a", "a", "a", "a", "a", "a");

    var cards = new List<Card>
    {
        Card.Create(user.Id, "a", "a"),
        Card.Create(user.Id, "a", "a"),
        Card.Create(user.Id, "a", "a"),
        Card.Create(user.Id, "a", "a"),
        Card.Create(user.Id, "a", "a"),
        Card.Create(user.Id, "a", "a"),
    };
    
    var deck = Deck.Create("a", "a", true, "a", user.Id);
    
    dbContext.Cards.AddRange(cards);
    dbContext.Decks.Add(deck);
    deck.AddCard(cards[0]);
    deck.AddCard(cards[1]);
    deck.AddCard(cards[2]);
    deck.AddCard(cards[3]);
    deck.AddCard(cards[4]);
    deck.AddCard(cards[5]);

    dbContext.Users.Add(user);
    var deckStatistic = deck.AddStatistic(user);
    deckStatistic.IncreaseStatistic(1, 1);
    
    await dbContext.SaveChangesAsync();

    var users = dbContext.Users.Select(u => u).ToList();
    Console.WriteLine(users[0].RefreshToken == null);
});

app.Map("reboot", async context =>
{
    var dbContext = context.RequestServices.GetService<ProCardsDbContext>()!;
    await dbContext.Database.EnsureDeletedAsync();
    await dbContext.Database.EnsureCreatedAsync();
});

app.UseCookieAuthentication();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();