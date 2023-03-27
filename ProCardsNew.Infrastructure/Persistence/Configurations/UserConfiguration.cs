using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProCardsNew.Domain.DeckAggregate.Entities;
using ProCardsNew.Domain.UserAggregate;
using ProCardsNew.Domain.UserAggregate.Entities;
using ProCardsNew.Domain.UserAggregate.ValueObjects;

namespace ProCardsNew.Infrastructure.Persistence.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        ConfigureUser(builder);
        ConfigureDeckStatistic(builder);
        ConfigureUserDeck(builder);
        ConfigureStatistic(builder);
    }

    private void ConfigureUser(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");

        builder.HasKey(u => u.Id);

        builder.Property(u => u.Id)
            .ValueGeneratedNever()
            .HasConversion(
                id => id.Value,
                value => UserId.Create(value));

        builder.Property(u => u.RefreshToken)
            .ValueGeneratedNever()
            .HasConversion(
                id => id != null ? id.Value : null,
                value => value != null ? RefreshToken.Create(value) : null);
    }

    private void ConfigureDeckStatistic(EntityTypeBuilder<User> builder)
    {
        builder.HasMany(u => u.DeckStatistics)
            .WithOne(ds => ds.User);

        builder.HasMany(u => u.LeaderboardWithUserDecks)
            .WithMany(d => d.LeaderboardUsers)
            .UsingEntity<DeckStatistic>();
    }

    private void ConfigureUserDeck(EntityTypeBuilder<User> builder)
    {
        builder.HasMany(u => u.UserDecks)
            .WithOne(ud => ud.User);

        builder.HasMany(u => u.Decks)
            .WithMany(d => d.Users)
            .UsingEntity<UserDeck>();
    }

    private void ConfigureStatistic(EntityTypeBuilder<User> builder)
    {
        builder.HasOne(u => u.Statistic)
            .WithOne(s => s.User);
    }
}