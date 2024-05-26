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

        builder.HasKey(i => i.Id);

        builder.Property(i => i.CardId)
            .ValueGeneratedNever()
            .HasConversion(
                id => id.Value,
                value => CardId.Create(value));

        builder.Property(c => c.Id)
            .ValueGeneratedNever()
            .HasConversion(
                id => id.Value,
                value => ImageId.Create(value));
        
        ConfigureProperties(builder);
    }

    private void ConfigureProperties(EntityTypeBuilder<Image> builder)
    {
        builder.Property(i => i.S3ImageId);
        
        builder.Property(i => i.Name)
            .IsRequired()
            .HasMaxLength(_validationSettings.ImageNameLength);

        builder.Property(i => i.FileExtension)
            .IsRequired()
            .HasMaxLength(_validationSettings.ImageFileExtensionLength);

        builder.Property(i => i.CreatedAt)
            .IsRequired();
    }
}