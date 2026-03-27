namespace LogisticsSystem.Application.DTOs;

public class ShipmentDto
{
    public int Id { get; set; }
    public string ShipmentNo { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
    public string Mode { get; set; } = string.Empty;
    public int ShipperId { get; set; }
    public string ShipperName { get; set; } = string.Empty;
    public int? ConsigneeId { get; set; }
    public string? ConsigneeName { get; set; }
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
    public string? DeclarationNo { get; set; }
    public string Status { get; set; } = string.Empty;
    public string CustomsStatus { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public List<ContainerDto> Containers { get; set; } = new();
    public List<ShipmentTrackingDto> Trackings { get; set; } = new();
}

public class CreateShipmentDto
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

public class UpdateShipmentDto
{
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

public class ContainerDto
{
    public int Id { get; set; }
    public string ContainerNo { get; set; } = string.Empty;
    public string? ContainerType { get; set; }
    public string? SealNo { get; set; }
    public decimal? GrossWeight { get; set; }
    public string Status { get; set; } = string.Empty;
}

public class ShipmentTrackingDto
{
    public int Id { get; set; }
    public string LocationCode { get; set; } = string.Empty;
    public string LocationName { get; set; } = string.Empty;
    public string EventCode { get; set; } = string.Empty;
    public string? Note { get; set; }
    public DateTime EventDate { get; set; }
}