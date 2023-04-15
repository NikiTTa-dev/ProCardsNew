using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.Options;
using ProCardsNew.Application.Common.Settings;
using ProCardsNew.Domain.CardAggregate.Entities;
using ProCardsNew.Domain.CardAggregate.ValueObjects;

namespace ProCardsNew.Infrastructure.Persistence.Configurations;

public class ImageConfiguration : IEntityTypeConfiguration<Image>
{
    private readonly ValidationSettings _validationSettings;

    public ImageConfiguration(IOptions<ValidationSettings> validationSettings)
    {
        _validationSettings = validationSettings.Value;
    }
    
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

        builder.HasOne(i => i.Card)
            .WithMany(c => c.Images);
        
        ConfigureProperties(builder);
    }

    private void ConfigureProperties(EntityTypeBuilder<Image> builder)
    {
        builder.Property(i => i.Name)
            .IsRequired()
            .HasMaxLength(_validationSettings.ImageNameLength);

        builder.Property(i => i.FileExtension)
            .IsRequired()
            .HasMaxLength(_validationSettings.ImageFileExtensionLength);

        builder.Property(i => i.Data)
            .IsRequired();

        builder.Property(i => i.CreatedAt)
            .IsRequired();
    }
}