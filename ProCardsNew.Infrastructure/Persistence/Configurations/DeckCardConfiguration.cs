using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProCardsNew.Domain.CardAggregate.ValueObjects;
using ProCardsNew.Domain.DeckAggregate.Entities;
using ProCardsNew.Domain.DeckAggregate.ValueObjects;

namespace ProCardsNew.Infrastructure.Persistence.Configurations;

public class DeckCardConfiguration : IEntityTypeConfiguration<DeckCard>
{
    public void Configure(EntityTypeBuilder<DeckCard> builder)
    {
        builder.ToTable("DeckCard");

        builder.HasKey(dc=> new {dc.CardId, dc.DeckId});

        builder.Property(dc => dc.DeckId)
            .ValueGeneratedOnAdd()
            .HasConversion(
                id => id.Value,
                value => DeckId.Create(value));
        
        builder.Property(dc => dc.CardId)
            .ValueGeneratedOnAdd()
            .HasConversion(
                id => id.Value,
                value => CardId.Create(value));

        builder.HasOne(dc => dc.Deck)
            .WithMany(d => d.DeckCards)
            .HasForeignKey(dc => dc.DeckId);

        builder.HasOne(dc => dc.Card)
            .WithMany(c => c.DeckCards)
            .HasForeignKey(dc => dc.CardId);
        
        ConfigureProperties(builder);
    }

    private void ConfigureProperties(EntityTypeBuilder<DeckCard> builder)
    {
        builder.Property(dc => dc.AddedAt)
            .IsRequired();
    }
}