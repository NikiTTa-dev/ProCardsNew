using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProCardsNew.Domain.DeckAggregate;
using ProCardsNew.Domain.DeckAggregate.Entities;
using ProCardsNew.Domain.DeckAggregate.ValueObjects;

namespace ProCardsNew.Infrastructure.Persistence.Configurations;

public class DeckConfiguration : IEntityTypeConfiguration<Deck>
{
    public void Configure(EntityTypeBuilder<Deck> builder)
    {
        builder.ToTable("Decks");

        builder.HasKey(d => d.Id);

        builder.Property(d => d.Id)
            .ValueGeneratedNever()
            .HasConversion(
                id => id.Value,
                value => DeckId.Create(value));

        builder.HasMany(d => d.DeckCards)
            .WithOne(dc => dc.Deck);

        builder.HasMany(d => d.Cards)
            .WithMany(c => c.Decks)
            .UsingEntity<DeckCard>();
    }
}