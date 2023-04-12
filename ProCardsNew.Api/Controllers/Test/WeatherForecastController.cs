using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProCardsNew.Api.Controllers.Common;
using ProCardsNew.Domain.CardAggregate;
using ProCardsNew.Domain.CardAggregate.Entities;
using ProCardsNew.Domain.UserAggregate.ValueObjects;
using ProCardsNew.Infrastructure.Persistence;

namespace ProCardsNew.Api.Controllers.Test;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ApiController
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    [HttpPost]
    public async Task<IActionResult> Post(IFormFile file)
    {
        await Task.CompletedTask;

        var context = HttpContext.RequestServices.GetService<ProCardsDbContext>()!;

        var card = Card.Create(UserId.Create(Guid.Parse("ccf4b043-a07b-457b-a7bb-5b5a4cb96aae")), "firstS", "backS");
        var side = Side.Create("Front");
        context.Add(card);
        context.Add(side);
        await context.SaveChangesAsync();
        using (var stream = new MemoryStream())
        {
            await file.CopyToAsync(stream);
            var image = Image.Create(
                card.Id,
                side.Id,
                file.FileName,
                file.ContentType,
                stream.ToArray());

            context.Add(image);
            await context.SaveChangesAsync();
        }


        //return File(file.OpenReadStream(), file.ContentType);

        var rng = new Random();
        return Ok(Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray());
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        await Task.CompletedTask;

        var context = HttpContext.RequestServices.GetService<ProCardsDbContext>()!;

        var mage = context.Cards
            .Include(c => c.Images)
            .Where(c => c.OwnerId == UserId.Create(Guid.Parse("ccf4b043-a07b-457b-a7bb-5b5a4cb96aae")))
            .SelectMany(c => c.Images)
            .Include(i => i.Side)
            .ToList();
        
        var image = mage     
            .FirstOrDefault(i => i.Side?.SideName == "Front");
        
        return File(image!.Data, image.FileExtension);
    }
}