using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProCardsNew.Domain.UserAggregate.Entities;
using ProCardsNew.Domain.UserAggregate.ValueObjects;

namespace ProCardsNew.Infrastructure.Persistence.Configurations;

public class StatisticConfiguration : IEntityTypeConfiguration<Statistic>
{
    public void Configure(EntityTypeBuilder<Statistic> builder)
    {
        builder.ToTable("Statistic");

        builder.HasKey(s => s.Id);

        builder.Property(s => s.Id)
            .ValueGeneratedNever()
            .HasConversion(
                id => id.Value,
                value => UserId.Create(value));

        builder.HasOne(s => s.User)
            .WithOne(u => u.Statistic)
            .HasForeignKey(typeof(Statistic));

        ConfigureProperties(builder);
    }

    private void ConfigureProperties(EntityTypeBuilder<Statistic> builder)
    {
        builder.Property(s => s.CardsViewed)
            .IsRequired();

        builder.Property(s => s.Hours)
            .IsRequired();

        builder.Property(s => s.CardsCreated)
            .IsRequired();

        builder.Property(s => s.Score)
            .IsRequired();
    }
}