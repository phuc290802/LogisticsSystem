using LogisticsSystem.Domain.Entities;

namespace LogisticsSystem.Domain.Interfaces;

public interface IShipmentRepository : IRepository<Shipment>
{
    Task<Shipment?> GetByShipmentNoAsync(string shipmentNo, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Shipment>> GetByShipperIdAsync(int shipperId, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Shipment>> GetByStatusAsync(ShipmentStatus status, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Shipment>> GetByDateRangeAsync(DateTime from, DateTime to, CancellationToken cancellationToken = default);
    Task<bool> IsShipmentNoExistsAsync(string shipmentNo, int? excludeId = null, CancellationToken cancellationToken = default);
}