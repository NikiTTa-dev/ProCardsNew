using Microsoft.EntityFrameworkCore;
using ProCardsNew.Domain.CardAggregate;
using ProCardsNew.Domain.DeckAggregate;
using ProCardsNew.Domain.UserAggregate;

namespace ProCardsNew.Infrastructure.Persistence;

public class ProCardsDbContext : DbContext
{
    public ProCardsDbContext(DbContextOptions<ProCardsDbContext> options)
        : base(options)
    {
    }

    public DbSet<Deck> Decks { get; init; } = null!;
    public DbSet<Card> Cards { get; init; } = null!;
    public DbSet<User> Users { get; init; } = null!; 

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .ApplyConfigurationsFromAssembly(typeof(ProCardsDbContext).Assembly);
        
        base.OnModelCreating(modelBuilder);
    }
}