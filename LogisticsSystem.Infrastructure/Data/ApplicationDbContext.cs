using LogisticsSystem.Domain.Entities;
using LogisticsSystem.Domain.Common;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace LogisticsSystem.Infrastructure.Data;

public class ApplicationDbContext : DbContext
{
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Shipment> Shipments { get; set; }
    public DbSet<Container> Containers { get; set; }
    public DbSet<Document> Documents { get; set; }
    public DbSet<Cost> Costs { get; set; }
    public DbSet<ShipmentTracking> ShipmentTrackings { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
    : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        // Configure global query filter for soft delete
        modelBuilder.Entity<Customer>().HasQueryFilter(e => !e.IsDeleted);
        modelBuilder.Entity<Shipment>().HasQueryFilter(e => !e.IsDeleted);
        modelBuilder.Entity<Container>().HasQueryFilter(e => !e.IsDeleted);
        modelBuilder.Entity<Document>().HasQueryFilter(e => !e.IsDeleted);
        modelBuilder.Entity<Cost>().HasQueryFilter(e => !e.IsDeleted);
        modelBuilder.Entity<ShipmentTracking>().HasQueryFilter(e => !e.IsDeleted);

        base.OnModelCreating(modelBuilder);
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        // Update timestamps
        var entries = ChangeTracker
            .Entries()
            .Where(e => e.Entity is BaseEntity &&
                       (e.State == EntityState.Added || e.State == EntityState.Modified));

        foreach (var entityEntry in entries)
        {
            if (entityEntry.State == EntityState.Modified)
            {
                ((BaseEntity)entityEntry.Entity).UpdateTimestamp();
            }
        }

        // Dispatch domain events
        var domainEvents = ChangeTracker
            .Entries<IAggregateRoot>()
            .SelectMany(x => x.Entity.DomainEvents)
            .ToList();

        foreach (var aggregateRoot in ChangeTracker.Entries<IAggregateRoot>())
        {
            aggregateRoot.Entity.ClearDomainEvents();
        }

        var result = await base.SaveChangesAsync(cancellationToken);

        // Dispatch events after save
        foreach (var domainEvent in domainEvents)
        {
            // You can dispatch via MediatR here
        }

        return result;
    }
}