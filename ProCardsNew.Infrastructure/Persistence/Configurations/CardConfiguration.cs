using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.Options;
using ProCardsNew.Application.Common.Settings;
using ProCardsNew.Domain.CardAggregate;
using ProCardsNew.Domain.CardAggregate.ValueObjects;
using ProCardsNew.Domain.DeckAggregate.Entities;
using ProCardsNew.Domain.UserAggregate.ValueObjects;

namespace ProCardsNew.Infrastructure.Persistence.Configurations;

public class CardConfiguration : IEntityTypeConfiguration<Card>
{
    private readonly ValidationSettings _validationSettings;

    public CardConfiguration(IOptions<ValidationSettings> validationSettings)
    {
        _validationSettings = validationSettings.Value;
    }
    
    public void Configure(EntityTypeBuilder<Card> builder)
    {
        ConfigureCard(builder);
        ConfigureProperties(builder);
        ConfigureDeckCard(builder);
        ConfigureGrade(builder);
        ConfigureOwner(builder);
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
    
    private void ConfigureProperties(EntityTypeBuilder<Card> builder)
    {
        builder.Property(c => c.FrontSide)
            .HasMaxLength(_validationSettings.CardSideLength)
            .IsRequired();

        builder.Property(c => c.BackSide)
            .HasMaxLength(_validationSettings.CardSideLength)
            .IsRequired();

        builder.Property(c => c.OwnerId)
            .IsRequired();

        builder.Property(c => c.CreatedAtDateTime)
            .IsRequired();

        builder.Property(c => c.UpdatedAtDateTime)
            .IsRequired();
    }
    
    private void ConfigureOwner(EntityTypeBuilder<Card> builder)
    {
        builder.HasOne(c => c.Owner)
            .WithMany(u => u.OwnedCards)
            .HasForeignKey(c => c.OwnerId);

        builder.Property(c => c.OwnerId)
            .ValueGeneratedNever()
            .HasConversion(
                id => id.Value,
                value => UserId.Create(value));
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