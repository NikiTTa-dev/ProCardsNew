using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProCardsNew.Domain.CardAggregate.ValueObjects;

namespace ProCardsNew.Infrastructure.Persistence.Configurations;

public class ImageDataConfiguration: IEntityTypeConfiguration<ImageData>
{
    public void Configure(EntityTypeBuilder<ImageData> builder)
    {
        builder.ToTable("ImagesData");

        builder.HasKey(id => id.ImageId);
        
        builder.Property(id => id.ImageId)
            .ValueGeneratedNever()
            .HasConversion(
                id => id!.Value,
                value => ImageId.Create(value));

        builder.Property(id => id.Data)
            .IsRequired();
    }
}