using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProCardsNew.Domain.CardAggregate.Entities;
using ProCardsNew.Domain.CardAggregate.ValueObjects;
using ProCardsNew.Infrastructure.Settings;

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
        
        ConfigureProperties(builder);
    }

    private void ConfigureProperties(EntityTypeBuilder<Side> builder)
    {
        builder.Property(sd => sd.SideName)
            .IsRequired()
            .HasMaxLength(DbContextEntitiesSettings.SideNameLength);
    }
}