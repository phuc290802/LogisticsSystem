using LogisticsSystem.Domain.Common;
using LogisticsSystem.Domain.Events;
using LogisticsSystem.Domain.ValueObjects;
using System.ComponentModel;
using System.Reflection.Metadata;

namespace LogisticsSystem.Domain.Entities;

public class Shipment : BaseEntity, IAggregateRoot
{
    private readonly List<Container> _containers = new();
    private readonly List<Document> _documents = new();
    private readonly List<Cost> _costs = new();
    private readonly List<ShipmentTracking> _trackings = new();
    private readonly List<IDomainEvent> _domainEvents = new();

    public string ShipmentNo { get; private set; }
    public ShipmentType Type { get; private set; }
    public TransportMode Mode { get; private set; }

    // References
    public int ShipperId { get; private set; }
    public int? ConsigneeId { get; private set; }
    public int? ForwarderId { get; private set; }

    // Navigation properties
    public virtual Customer Shipper { get; private set; }
    public virtual Customer? Consignee { get; private set; }
    public virtual Customer? Forwarder { get; private set; }

    // Cargo info
    public string? GoodsDescription { get; private set; }
    public decimal? Quantity { get; private set; }
    public decimal? Weight { get; private set; } // kg
    public decimal? Volume { get; private set; } // CBM
    public string? PackageType { get; private set; }

    // Transport info
    public string? VesselName { get; private set; }
    public string? VoyageNo { get; private set; }
    public string? Pol { get; private set; } // Port of Loading
    public string? Pod { get; private set; } // Port of Discharge
    public DateTime? Etd { get; private set; }
    public DateTime? Eta { get; private set; }

    // Customs
    public string? DeclarationNo { get; private set; }
    public CustomsStatus CustomsStatus { get; private set; }

    // Status
    public ShipmentStatus Status { get; private set; }

    // Collections
    public IReadOnlyCollection<Container> Containers => _containers.AsReadOnly();
    public IReadOnlyCollection<Document> Documents => _documents.AsReadOnly();
    public IReadOnlyCollection<Cost> Costs => _costs.AsReadOnly();
    public IReadOnlyCollection<ShipmentTracking> Trackings => _trackings.AsReadOnly();

    public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

    private Shipment() { } // EF Core

    public Shipment(string shipmentNo, ShipmentType type, TransportMode mode, int shipperId)
    {
        if (string.IsNullOrWhiteSpace(shipmentNo))
            throw new ArgumentException("Shipment number is required", nameof(shipmentNo));

        ShipmentNo = shipmentNo;
        Type = type;
        Mode = mode;
        ShipperId = shipperId;
        Status = ShipmentStatus.Draft;
        CustomsStatus = CustomsStatus.Pending;

        AddDomainEvent(new ShipmentCreatedEvent(this));
    }

    public void SetCargoInfo(string? description, decimal? quantity, decimal? weight, decimal? volume, string? packageType)
    {
        GoodsDescription = description;
        Quantity = quantity;
        Weight = weight;
        Volume = volume;
        PackageType = packageType;
        UpdateTimestamp();
    }

    public void SetTransportInfo(string? vesselName, string? voyageNo, string? pol, string? pod, DateTime? etd, DateTime? eta)
    {
        VesselName = vesselName;
        VoyageNo = voyageNo;
        Pol = pol;
        Pod = pod;
        Etd = etd;
        Eta = eta;
        UpdateTimestamp();
    }

    public void SetConsignee(int consigneeId)
    {
        ConsigneeId = consigneeId;
        UpdateTimestamp();
    }

    public void SetForwarder(int forwarderId)
    {
        ForwarderId = forwarderId;
        UpdateTimestamp();
    }

    public void AddContainer(string containerNo, string? containerType, string? sealNo, decimal? grossWeight)
    {
        var container = new Container(containerNo, containerType, sealNo, grossWeight);
        _containers.Add(container);
        UpdateTimestamp();

        AddDomainEvent(new ContainerAddedEvent(this, container));
    }

    public void AddDocument(string docType, string? docNumber, DateTime? issueDate, DateTime? expiryDate, string? filePath)
    {
        var document = new Document(docType, docNumber, issueDate, expiryDate, filePath);
        _documents.Add(document);
        UpdateTimestamp();
    }

    public void AddCost(CostType costType, string feeCategory, int customerId, string currency, decimal amount, decimal? exchangeRate, string? description)
    {
        var cost = new Cost(costType, feeCategory, customerId, currency, amount, exchangeRate, description);
        _costs.Add(cost);
        UpdateTimestamp();
    }

    public void AddTracking(string locationCode, string locationName, ShipmentEventCode eventCode, string? note)
    {
        var tracking = new ShipmentTracking(locationCode, locationName, eventCode, note);
        _trackings.Add(tracking);
        UpdateTimestamp();

        AddDomainEvent(new ShipmentStatusChangedEvent(this, Status, GetStatusFromEvent(eventCode)));

        // Update status based on tracking event
        Status = GetStatusFromEvent(eventCode);
    }

    public void UpdateCustomsInfo(string declarationNo, CustomsStatus status)
    {
        DeclarationNo = declarationNo;
        CustomsStatus = status;
        UpdateTimestamp();
    }

    private ShipmentStatus GetStatusFromEvent(ShipmentEventCode eventCode)
    {
        return eventCode switch
        {
            ShipmentEventCode.Draft => ShipmentStatus.Draft,
            ShipmentEventCode.Booked => ShipmentStatus.Booked,
            ShipmentEventCode.GateIn => ShipmentStatus.InTransit,
            ShipmentEventCode.Loaded => ShipmentStatus.InTransit,
            ShipmentEventCode.Departed => ShipmentStatus.InTransit,
            ShipmentEventCode.Arrived => ShipmentStatus.InTransit,
            ShipmentEventCode.CustomsCleared => ShipmentStatus.InTransit,
            ShipmentEventCode.Delivered => ShipmentStatus.Delivered,
            _ => ShipmentStatus.Draft
        };
    }

    private void AddDomainEvent(IDomainEvent domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }

    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }
}