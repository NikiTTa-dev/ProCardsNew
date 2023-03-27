using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProCardsNew.Domain.CardAggregate;
using ProCardsNew.Domain.CardAggregate.ValueObjects;
using ProCardsNew.Domain.DeckAggregate.Entities;

namespace ProCardsNew.Infrastructure.Persistence.Configurations;

public class CardConfiguration : IEntityTypeConfiguration<Card>
{
    public void Configure(EntityTypeBuilder<Card> builder)
    {
        ConfigureCard(builder);
        ConfigureDeckCard(builder);
        ConfigureGrade(builder);
    }

    private void ConfigureCard(EntityTypeBuilder<Card> builder)
    {
        builder.ToTable("Cards");

        builder.HasKey(c => c.Id);

        builder.Property(c => c.Id)
            .ValueGeneratedNever()
            .HasConversion(
                id => id.Value,
                value => CardId.Create(value));
    }

    private void ConfigureDeckCard(EntityTypeBuilder<Card> builder)
    {
        builder.HasMany(c => c.DeckCards)
            .WithOne(dc => dc.Card);

        builder.HasMany(c => c.Decks)
            .WithMany(d => d.Cards)
            .UsingEntity<DeckCard>();
    }
    
    private void ConfigureGrade(EntityTypeBuilder<Card> builder)
    {
        builder.HasMany(c => c.Grades)
            .WithOne(g => g.Card);
    }
}