using LogisticsSystem.Domain.Entities;
using LogisticsSystem.Domain.Interfaces;
using LogisticsSystem.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace LogisticsSystem.Infrastructure.Repositories;

public class ShipmentRepository : GenericRepository<Shipment>, IShipmentRepository
{
    public ShipmentRepository(ApplicationDbContext context) : base(context)
    {
    }

    public override async Task<Shipment?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Include(s => s.Shipper)
            .Include(s => s.Consignee)
            .Include(s => s.Forwarder)
            .Include(s => s.Containers)
            .Include(s => s.Trackings)
            .FirstOrDefaultAsync(s => s.Id == id, cancellationToken);
    }

    public async Task<Shipment?> GetByShipmentNoAsync(string shipmentNo, CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Include(s => s.Shipper)
            .Include(s => s.Consignee)
            .FirstOrDefaultAsync(s => s.ShipmentNo == shipmentNo, cancellationToken);
    }

    public async Task<IReadOnlyList<Shipment>> GetByShipperIdAsync(int shipperId, CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Include(s => s.Shipper)
            .Include(s => s.Consignee)
            .Where(s => s.ShipperId == shipperId)
            .OrderByDescending(s => s.CreatedAt)
            .ToListAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<Shipment>> GetByStatusAsync(ShipmentStatus status, CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Include(s => s.Shipper)
            .Where(s => s.Status == status)
            .OrderByDescending(s => s.CreatedAt)
            .ToListAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<Shipment>> GetByDateRangeAsync(DateTime from, DateTime to, CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Include(s => s.Shipper)
            .Where(s => s.CreatedAt >= from && s.CreatedAt <= to)
            .OrderByDescending(s => s.CreatedAt)
            .ToListAsync(cancellationToken);
    }

    public async Task<bool> IsShipmentNoExistsAsync(string shipmentNo, int? excludeId = null, CancellationToken cancellationToken = default)
    {
        var query = _dbSet.Where(s => s.ShipmentNo == shipmentNo);

        if (excludeId.HasValue)
        {
            query = query.Where(s => s.Id != excludeId.Value);
        }

        return await query.AnyAsync(cancellationToken);
    }
}