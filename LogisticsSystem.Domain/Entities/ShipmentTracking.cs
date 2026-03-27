using LogisticsSystem.Domain.Common;

namespace LogisticsSystem.Domain.Entities;

public class ShipmentTracking : BaseEntity
{
    public int ShipmentId { get; private set; }
    public string LocationCode { get; private set; }
    public string LocationName { get; private set; }
    public ShipmentEventCode EventCode { get; private set; }
    public string? Note { get; private set; }
    public DateTime EventDate { get; private set; }

    public virtual Shipment Shipment { get; private set; }

    private ShipmentTracking() { }

    public ShipmentTracking(string locationCode, string locationName, ShipmentEventCode eventCode, string? note)
    {
        LocationCode = locationCode;
        LocationName = locationName;
        EventCode = eventCode;
        Note = note;
        EventDate = DateTime.UtcNow;
    }
}