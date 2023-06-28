using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.Options;
using ProCardsNew.Application.Common.Settings;
using ProCardsNew.Domain.DeckAggregate.Entities;
using ProCardsNew.Domain.UserAggregate;
using ProCardsNew.Domain.UserAggregate.ValueObjects;

namespace ProCardsNew.Infrastructure.Persistence.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    private readonly ValidationSettings _validationSettings;

    public UserConfiguration(IOptions<ValidationSettings> validationSettings)
    {
        _validationSettings = validationSettings.Value;
    }
    
    public void Configure(EntityTypeBuilder<User> builder)
    {
        ConfigureUser(builder);
        ConfigureProperties(builder);
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

    private void ConfigureProperties(EntityTypeBuilder<User> builder)
    {
        builder.Property(u => u.Login)
            .IsRequired()
            .HasMaxLength(_validationSettings.UserLoginLength);

        builder.Property(u => u.Email)
            .IsRequired()
            .HasMaxLength(_validationSettings.UserEmailLength);

        builder.Property(u => u.FirstName)
            .IsRequired()
            .HasMaxLength(_validationSettings.UserFirstNameLength);

        builder.Property(u => u.LastName)
            .IsRequired()
            .HasMaxLength(_validationSettings.UserLastNameLength);

        builder.Property(u => u.Location)
            .IsRequired()
            .HasMaxLength(_validationSettings.UserLocationLength);

        builder.Property(u => u.AvatarNumber)
            .IsRequired();

        builder.Property(u => u.RefreshToken)
            .HasMaxLength(_validationSettings.UserRefreshTokenLength);

        builder.Property(u => u.PasswordRecoveryCode)
            .HasMaxLength(_validationSettings.UserRecoveryCodeLength);

        builder.Property(u => u.PasswordRecoveryFailedCount)
            .IsRequired();

        builder.Property(u => u.PasswordHash)
            .IsRequired()
            .HasMaxLength(_validationSettings.UserPasswordHashLength);

        builder.Property(u => u.AccessFailedCount)
            .IsRequired();

        builder.Property(u => u.CreatedAtDateTime)
            .IsRequired();

        builder.Property(u => u.UpdatedAtDateTime)
            .IsRequired();
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
    }

    private void ConfigureStatistic(EntityTypeBuilder<User> builder)
    {
        builder.HasOne(u => u.Statistic)
            .WithOne(s => s.User);
    }
}