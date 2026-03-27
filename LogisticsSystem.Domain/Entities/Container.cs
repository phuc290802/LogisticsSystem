using LogisticsSystem.Domain.Common;

namespace LogisticsSystem.Domain.Entities;

public class Container : BaseEntity
{
    public int ShipmentId { get; private set; }
    public string ContainerNo { get; private set; }
    public string? ContainerType { get; private set; }
    public string? SealNo { get; private set; }
    public decimal? GrossWeight { get; private set; }
    public ContainerStatus Status { get; private set; }

    public virtual Shipment Shipment { get; private set; }

    private Container() { }

    public Container(string containerNo, string? containerType, string? sealNo, decimal? grossWeight)
    {
        ContainerNo = containerNo;
        ContainerType = containerType;
        SealNo = sealNo;
        GrossWeight = grossWeight;
        Status = ContainerStatus.Empty;
    }

    public void UpdateStatus(ContainerStatus status)
    {
        Status = status;
        UpdateTimestamp();
    }
}

public enum ContainerStatus
{
    Empty = 1,
    Loaded = 2,
    GateIn = 3,
    GateOut = 4
}