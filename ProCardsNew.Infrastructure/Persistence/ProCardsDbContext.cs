using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using ProCardsNew.Application.Common.Settings;
using ProCardsNew.Domain.CardAggregate;
using ProCardsNew.Domain.CardAggregate.Entities;
using ProCardsNew.Domain.DeckAggregate;
using ProCardsNew.Domain.DeckAggregate.Entities;
using ProCardsNew.Domain.UserAggregate;
using ProCardsNew.Domain.UserAggregate.Entities;
using ProCardsNew.Infrastructure.Persistence.Extensions;

namespace ProCardsNew.Infrastructure.Persistence;

public class ProCardsDbContext : DbContext
{
    private readonly IOptions<ValidationSettings> _validationSettings;

    public ProCardsDbContext(
        DbContextOptions<ProCardsDbContext> options,
        IOptions<ValidationSettings> validationSettings)
        : base(options)
    {
        _validationSettings = validationSettings;
    }

    public DbSet<Deck> Decks { get; init; } = null!;
    public DbSet<Card> Cards { get; init; } = null!;
    public DbSet<User> Users { get; init; } = null!;
    public DbSet<Side> Sides { get; init; } = null!;
    public DbSet<DeckCard> DeckCards { get; init; } = null!;
    public DbSet<Image> Images { get; init; } = null!;
    public DbSet<Statistic> Statistics { get; init; } = null!;
    public DbSet<DeckAccess> DeckAccesses { get; init; } = null!;
    public DbSet<DeckStatistic> DeckStatistics { get; init; } = null!;
    public DbSet<UserDeck> UserDecks { get; init; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .ApplyConfigurationsFromAssemblyWithServiceInjection(
                Assembly.GetExecutingAssembly(), _validationSettings);

        base.OnModelCreating(modelBuilder);
    }
}