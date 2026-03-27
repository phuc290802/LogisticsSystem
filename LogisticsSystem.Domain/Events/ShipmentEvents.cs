using LogisticsSystem.Domain.Common;
using LogisticsSystem.Domain.Entities;

namespace LogisticsSystem.Domain.Events;

public class ShipmentCreatedEvent : DomainEvent
{
    public Shipment Shipment { get; }

    public ShipmentCreatedEvent(Shipment shipment)
    {
        Shipment = shipment;
    }
}

public class ShipmentStatusChangedEvent : DomainEvent
{
    public Shipment Shipment { get; }
    public ShipmentStatus OldStatus { get; }
    public ShipmentStatus NewStatus { get; }

    public ShipmentStatusChangedEvent(Shipment shipment, ShipmentStatus oldStatus, ShipmentStatus newStatus)
    {
        Shipment = shipment;
        OldStatus = oldStatus;
        NewStatus = newStatus;
    }
}

public class ContainerAddedEvent : DomainEvent
{
    public Shipment Shipment { get; }
    public Container Container { get; }

    public ContainerAddedEvent(Shipment shipment, Container container)
    {
        Shipment = shipment;
        Container = container;
    }
}