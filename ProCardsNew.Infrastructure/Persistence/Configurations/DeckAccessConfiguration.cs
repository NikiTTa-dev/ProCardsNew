using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProCardsNew.Domain.DeckAggregate.Entities;
using ProCardsNew.Domain.DeckAggregate.ValueObjects;

namespace ProCardsNew.Infrastructure.Persistence.Configurations;

public class DeckAccessConfiguration 
    : IEntityTypeConfiguration<DeckAccess>
{
    public void Configure(EntityTypeBuilder<DeckAccess> builder)
    {
        builder.ToTable("DeckAccess");

        builder.HasKey(da => da.Id);

        builder.Property(ad => ad.Id)
            .ValueGeneratedNever()
            .HasConversion(
                id => id.Value,
                value => DeckAccessId.Create(value));
        
        builder.Property(ad => ad.DeckId)
            .ValueGeneratedNever()
            .HasConversion(
                id => id.Value,
                value => DeckId.Create(value));

        builder.HasMany(ad => ad.UserDecks)
            .WithOne(ud => ud.DeckAccess)
            .HasForeignKey(ud => ud.DeckAccessId);
        
        ConfigureProperties(builder);
    }

    private void ConfigureProperties(EntityTypeBuilder<DeckAccess> builder)
    {
        builder.Property(ad => ad.IsAccessible)
            .IsRequired();
    }
}