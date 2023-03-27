using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProCardsNew.Domain.CardAggregate.Entities;
using ProCardsNew.Domain.CardAggregate.ValueObjects;

namespace ProCardsNew.Infrastructure.Persistence.Configurations;

public class SideConfiguration : IEntityTypeConfiguration<Side>
{
    public void Configure(EntityTypeBuilder<Side> builder)
    {
        builder.ToTable("Sides");

        builder.HasKey(sd => sd.Id);

        builder.Property(sd => sd.Id)
            .ValueGeneratedNever()
            .HasConversion(
                id => id.Value,
                value => SideId.Create(value));
    }
}