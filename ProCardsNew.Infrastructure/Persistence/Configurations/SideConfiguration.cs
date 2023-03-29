using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.Options;
using ProCardsNew.Application.Common.Settings;
using ProCardsNew.Domain.CardAggregate.Entities;
using ProCardsNew.Domain.CardAggregate.ValueObjects;

namespace ProCardsNew.Infrastructure.Persistence.Configurations;

public class SideConfiguration : IEntityTypeConfiguration<Side>
{
    private readonly ValidationSettings _validationSettings;

    public SideConfiguration(IOptions<ValidationSettings> validationSettings)
    {
        _validationSettings = validationSettings.Value;
    }
    
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
            .HasMaxLength(_validationSettings.SideNameLength);
    }
}