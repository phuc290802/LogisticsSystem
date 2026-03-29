using LogisticsSystem.Application.Common.Interfaces;
using LogisticsSystem.Application.DTOs;

namespace LogisticsSystem.Application.Features.Shipments.Queries;

public class GetShipmentByIdQuery : IQuery<ShipmentDto?>
{
    public int Id { get; set; }
}

public class GetAllShipmentsQuery : IQuery<IReadOnlyList<ShipmentDto>>
{
    public string? Type { get; set; }
    public string? Status { get; set; }
    public int? ShipperId { get; set; }
    public DateTime? FromDate { get; set; }
    public DateTime? ToDate { get; set; }
}

public class GetShipmentByNoQuery : IQuery<ShipmentDto?>
{
    public string ShipmentNo { get; set; } = string.Empty;
}