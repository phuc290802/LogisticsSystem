using LogisticsSystem.Domain.Common;

namespace LogisticsSystem.Domain.Entities;

public class Cost : BaseEntity
{
    public int ShipmentId { get; private set; }
    public CostType CostType { get; private set; }
    public string FeeCategory { get; private set; }
    public int CustomerId { get; private set; }
    public string Currency { get; private set; }
    public decimal Amount { get; private set; }
    public decimal? ExchangeRate { get; private set; }
    public string? Description { get; private set; }
    public decimal? Vat { get; private set; }
    public PaymentStatus PaymentStatus { get; private set; }

    public virtual Shipment Shipment { get; private set; }
    public virtual Customer Customer { get; private set; }

    private Cost() { }

    public Cost(CostType costType, string feeCategory, int customerId, string currency, decimal amount, decimal? exchangeRate, string? description)
    {
        CostType = costType;
        FeeCategory = feeCategory;
        CustomerId = customerId;
        Currency = currency;
        Amount = amount;
        ExchangeRate = exchangeRate ?? 1;
        Description = description;
        PaymentStatus = PaymentStatus.Unpaid;
    }

    public void MarkAsPaid()
    {
        PaymentStatus = PaymentStatus.Paid;
        UpdateTimestamp();
    }
}

public enum PaymentStatus
{
    Unpaid = 1,
    Paid = 2,
    Partial = 3
}