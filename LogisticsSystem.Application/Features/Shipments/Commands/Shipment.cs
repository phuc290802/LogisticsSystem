using LogisticsSystem.Application.Common.Interfaces;
using LogisticsSystem.Application.DTOs;

namespace LogisticsSystem.Application.Features.Shipments.Commands;

public class CreateShipmentCommand : ICommand<ShipmentDto>
{
    public string ShipmentNo { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
    public string Mode { get; set; } = string.Empty;
    public int ShipperId { get; set; }
    public int? ConsigneeId { get; set; }
    public string? GoodsDescription { get; set; }
    public decimal? Quantity { get; set; }
    public decimal? Weight { get; set; }
    public decimal? Volume { get; set; }
    public string? PackageType { get; set; }
}

public class UpdateShipmentCommand : ICommand<ShipmentDto>
{
    public int Id { get; set; }
    public string? GoodsDescription { get; set; }
    public decimal? Quantity { get; set; }
    public decimal? Weight { get; set; }
    public decimal? Volume { get; set; }
    public string? PackageType { get; set; }
    public string? VesselName { get; set; }
    public string? VoyageNo { get; set; }
    public string? Pol { get; set; }
    public string? Pod { get; set; }
    public DateTime? Etd { get; set; }
    public DateTime? Eta { get; set; }
}

public class DeleteShipmentCommand : ICommand<bool>
{
    public int Id { get; set; }
}

public class AddContainerCommand : ICommand<ContainerDto>
{
    public int ShipmentId { get; set; }
    public string ContainerNo { get; set; } = string.Empty;
    public string? ContainerType { get; set; }
    public string? SealNo { get; set; }
    public decimal? GrossWeight { get; set; }
}

public class AddTrackingCommand : ICommand<ShipmentTrackingDto>
{
    public int ShipmentId { get; set; }
    public string LocationCode { get; set; } = string.Empty;
    public string LocationName { get; set; } = string.Empty;
    public string EventCode { get; set; } = string.Empty;
    public string? Note { get; set; }
}

public class UpdateCustomsInfoCommand : ICommand<bool>
{
    public int ShipmentId { get; set; }
    public string DeclarationNo { get; set; } = string.Empty;
    public string CustomsStatus { get; set; } = string.Empty;
}