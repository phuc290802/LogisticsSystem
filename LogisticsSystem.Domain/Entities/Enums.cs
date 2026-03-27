namespace LogisticsSystem.Domain.Entities;

public enum ShipmentType
{
    Import = 1,
    Export = 2
}

public enum TransportMode
{
    Air = 1,
    Sea = 2,
    Truck = 3,
    Rail = 4
}

public enum ShipmentStatus
{
    Draft = 1,
    Booked = 2,
    InTransit = 3,
    Delivered = 4,
    Closed = 5,
    Cancelled = 6
}

public enum CustomsStatus
{
    Pending = 1,
    Cleared = 2,
    Inspected = 3, // Vàng/Đỏ
    Rejected = 4
}

public enum CostType
{
    Cost = 1,    // Chi phí mua vào
    Revenue = 2  // Doanh thu bán ra
}

public enum ShipmentEventCode
{
    Draft = 1,
    Booked = 2,
    GateIn = 3,
    Loaded = 4,
    Departed = 5,
    Arrived = 6,
    CustomsCleared = 7,
    Delivered = 8
}