﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProCardsNew.Domain.DeckAggregate;
using ProCardsNew.Domain.DeckAggregate.Entities;
using ProCardsNew.Domain.DeckAggregate.ValueObjects;
using ProCardsNew.Domain.UserAggregate.Entities;
using ProCardsNew.Domain.UserAggregate.ValueObjects;
using ProCardsNew.Infrastructure.Settings;

namespace ProCardsNew.Infrastructure.Persistence.Configurations;

public class DeckConfiguration : IEntityTypeConfiguration<Deck>
{
    public void Configure(EntityTypeBuilder<Deck> builder)
    {
        ConfigureDeck(builder);
        ConfigureProperties(builder);
        ConfigureDeckCard(builder);
        ConfigureDeckStatistic(builder);
        ConfigureUserDeck(builder);
        ConfigureOwner(builder);
    }

    private void ConfigureDeck(EntityTypeBuilder<Deck> builder)
    {
        builder.ToTable("Decks");

        builder.HasKey(d => d.Id);

        builder.Property(d => d.Id)
            .ValueGeneratedNever()
            .HasConversion(
                id => id.Value,
                value => DeckId.Create(value));
    }
    
    private void ConfigureProperties(EntityTypeBuilder<Deck> builder)
    {
        builder.Property(d => d.Name)
            .IsRequired()
            .HasMaxLength(DbContextEntitiesSettings.DeckNameLength);

        builder.Property(d => d.Description)
            .IsRequired()
            .HasMaxLength(DbContextEntitiesSettings.DeckDescriptionLength);

        builder.Property(d => d.PasswordHash)
            .IsRequired()
            .HasMaxLength(DbContextEntitiesSettings.DeckPasswordHashLength);

        builder.Property(d => d.IsPublic)
            .IsRequired();

        builder.Property(d => d.CardsCount)
            .IsRequired();

        builder.Property(d => d.CreatedAtDateTime)
            .IsRequired();

        builder.Property(d => d.UpdatedAtDateTime)
            .IsRequired();
    }

    private void ConfigureOwner(EntityTypeBuilder<Deck> builder)
    {
        builder.HasOne(d => d.Owner)
            .WithMany(u => u.OwnedDecks)
            .HasForeignKey(d => d.OwnerId);

        builder.Property(d => d.OwnerId)
            .ValueGeneratedNever()
            .HasConversion(
                id => id.Value,
                value => UserId.Create(value));
    }

    private void ConfigureDeckCard(EntityTypeBuilder<Deck> builder)
    {
        builder.HasMany(d => d.DeckCards)
            .WithOne(dc => dc.Deck);

        builder.HasMany(d => d.Cards)
            .WithMany(c => c.Decks)
            .UsingEntity<DeckCard>();
    }

    private void ConfigureDeckStatistic(EntityTypeBuilder<Deck> builder)
    {
        builder.HasMany(d => d.DeckStatistics)
            .WithOne(ds => ds.Deck);

        builder.HasMany(d => d.LeaderboardUsers)
            .WithMany(u => u.LeaderboardWithUserDecks)
            .UsingEntity<DeckStatistic>();
    }

    private void ConfigureUserDeck(EntityTypeBuilder<Deck> builder)
    {
        builder.HasMany(d => d.UserDecks)
            .WithOne(ud => ud.Deck);

        builder.HasMany(d => d.Users)
            .WithMany(u => u.Decks)
            .UsingEntity<UserDeck>();
    }
}