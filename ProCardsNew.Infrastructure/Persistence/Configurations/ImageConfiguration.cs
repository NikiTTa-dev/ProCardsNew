using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProCardsNew.Domain.CardAggregate.Entities;
using ProCardsNew.Domain.CardAggregate.ValueObjects;
using ProCardsNew.Infrastructure.Settings;

namespace ProCardsNew.Infrastructure.Persistence.Configurations;

public class ImageConfiguration : IEntityTypeConfiguration<Image>
{
    public void Configure(EntityTypeBuilder<Image> builder)
    {
        builder.ToTable("Images");

        builder.HasKey(i => new { i.CardId, i.SideId });

        builder.Property(i => i.CardId)
            .ValueGeneratedNever()
            .HasConversion(
                id => id.Value,
                value => CardId.Create(value));

        builder.Property(i => i.SideId)
            .ValueGeneratedNever()
            .HasConversion(
                id => id.Value,
                value => SideId.Create(value));

        builder.HasOne(i => i.Side);
        
        ConfigureProperties(builder);
    }

    private void ConfigureProperties(EntityTypeBuilder<Image> builder)
    {
        builder.Property(i => i.Name)
            .IsRequired()
            .HasMaxLength(DbContextEntitiesSettings.ImageNameLength);

        builder.Property(i => i.FileExtension)
            .IsRequired()
            .HasMaxLength(DbContextEntitiesSettings.ImageFileExtensionLength);

        builder.Property(i => i.Data)
            .IsRequired();

        builder.Property(i => i.UpdatedAt)
            .IsRequired();
    }
}