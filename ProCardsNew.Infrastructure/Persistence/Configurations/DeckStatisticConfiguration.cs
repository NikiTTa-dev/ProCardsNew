using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProCardsNew.Domain.DeckAggregate.Entities;
using ProCardsNew.Domain.DeckAggregate.ValueObjects;
using ProCardsNew.Domain.UserAggregate.ValueObjects;

namespace ProCardsNew.Infrastructure.Persistence.Configurations;

public class DeckStatisticConfiguration : IEntityTypeConfiguration<DeckStatistic>
{
    public void Configure(EntityTypeBuilder<DeckStatistic> builder)
    {
        builder.ToTable("DeckStatistic");

        builder.HasKey(ds => new { ds.DeckId, ds.UserId });

        builder.Property(ds => ds.DeckId)
            .ValueGeneratedNever()
            .HasConversion(
                id => id.Value,
                value => DeckId.Create(value));

        builder.Property(ds => ds.UserId)
            .ValueGeneratedNever()
            .HasConversion(
                id => id.Value,
                value => UserId.Create(value));
    }
}