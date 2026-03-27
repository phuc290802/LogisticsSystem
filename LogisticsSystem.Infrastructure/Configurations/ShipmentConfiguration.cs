using LogisticsSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LogisticsSystem.Infrastructure.Configurations;

public class ShipmentConfiguration : IEntityTypeConfiguration<Shipment>
{
    public void Configure(EntityTypeBuilder<Shipment> builder)
    {
        builder.ToTable("Shipments");

        builder.HasKey(s => s.Id);

        builder.Property(s => s.ShipmentNo)
            .IsRequired()
            .HasMaxLength(50);

        builder.HasIndex(s => s.ShipmentNo)
            .IsUnique();

        builder.Property(s => s.Type)
            .IsRequired()
            .HasConversion<int>();

        builder.Property(s => s.Mode)
            .IsRequired()
            .HasConversion<int>();

        builder.Property(s => s.Status)
            .IsRequired()
            .HasConversion<int>()
            .HasDefaultValue(ShipmentStatus.Draft);

        builder.Property(s => s.CustomsStatus)
            .IsRequired()
            .HasConversion<int>()
            .HasDefaultValue(CustomsStatus.Pending);

        builder.Property(s => s.Weight)
            .HasPrecision(18, 2);

        builder.Property(s => s.Volume)
            .HasPrecision(18, 2);

        builder.Property(s => s.GoodsDescription)
            .HasMaxLength(1000);

        // Relationships
        builder.HasOne(s => s.Shipper)
            .WithMany()
            .HasForeignKey(s => s.ShipperId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(s => s.Consignee)
            .WithMany()
            .HasForeignKey(s => s.ConsigneeId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(s => s.Forwarder)
            .WithMany()
            .HasForeignKey(s => s.ForwarderId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(s => s.Containers)
            .WithOne(c => c.Shipment)
            .HasForeignKey(c => c.ShipmentId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(s => s.Documents)
            .WithOne(d => d.Shipment)
            .HasForeignKey(d => d.ShipmentId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(s => s.Costs)
            .WithOne(c => c.Shipment)
            .HasForeignKey(c => c.ShipmentId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(s => s.Trackings)
            .WithOne(t => t.Shipment)
            .HasForeignKey(t => t.ShipmentId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Ignore(s => s.DomainEvents);
    }
}