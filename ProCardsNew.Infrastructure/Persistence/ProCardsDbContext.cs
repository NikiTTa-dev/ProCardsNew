using Microsoft.EntityFrameworkCore;
using ProCardsNew.Domain.DeckAggregate;

namespace ProCardsNew.Infrastructure.Persistence;

public class ProCardsDbContext : DbContext
{
    public ProCardsDbContext(DbContextOptions<ProCardsDbContext> options)
        : base(options)
    {
    }

    public DbSet<Deck> Decks { get; init; } = null!;
}