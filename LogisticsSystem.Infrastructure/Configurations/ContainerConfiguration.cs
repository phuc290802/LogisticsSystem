using LogisticsSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LogisticsSystem.Infrastructure.Configurations;

public class ContainerConfiguration : IEntityTypeConfiguration<Container>
{
    public void Configure(EntityTypeBuilder<Container> builder)
    {
        builder.ToTable("Containers");

        builder.HasKey(c => c.Id);

        builder.Property(c => c.ContainerNo)
            .IsRequired()
            .HasMaxLength(20);

        builder.HasIndex(c => c.ContainerNo);

        builder.Property(c => c.ContainerType)
            .HasMaxLength(10);

        builder.Property(c => c.SealNo)
            .HasMaxLength(20);

        builder.Property(c => c.GrossWeight)
            .HasPrecision(18, 2);

        builder.Property(c => c.Status)
            .HasConversion<int>();
    }
}