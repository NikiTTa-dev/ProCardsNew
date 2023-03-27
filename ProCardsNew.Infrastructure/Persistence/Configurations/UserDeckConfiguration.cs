using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProCardsNew.Domain.DeckAggregate.ValueObjects;
using ProCardsNew.Domain.UserAggregate.Entities;
using ProCardsNew.Domain.UserAggregate.ValueObjects;

namespace ProCardsNew.Infrastructure.Persistence.Configurations;

public class UserDeckConfiguration : IEntityTypeConfiguration<UserDeck>
{
    public void Configure(EntityTypeBuilder<UserDeck> builder)
    {
        builder.ToTable("UserDeck");

        builder.HasKey(ud => new { ud.UserId, ud.DeckId, ud.OpenedAtDateTime });

        builder.Property(ud => ud.UserId)
            .ValueGeneratedNever()
            .HasConversion(
                id => id.Value,
                value => UserId.Create(value));

        builder.Property(ud => ud.DeckId)
            .ValueGeneratedNever()
            .HasConversion(
                id => id.Value,
                value => DeckId.Create(value));
    }
}