using LogisticsSystem.Domain.Common;

namespace LogisticsSystem.Domain.Entities;

public class Document : BaseEntity
{
    public int ShipmentId { get; private set; }
    public string DocType { get; private set; }
    public string? DocNumber { get; private set; }
    public DateTime? IssueDate { get; private set; }
    public DateTime? ExpiryDate { get; private set; }
    public string? FilePath { get; private set; }
    public DocumentStatus Status { get; private set; }

    public virtual Shipment Shipment { get; private set; }

    private Document() { }

    public Document(string docType, string? docNumber, DateTime? issueDate, DateTime? expiryDate, string? filePath)
    {
        DocType = docType;
        DocNumber = docNumber;
        IssueDate = issueDate;
        ExpiryDate = expiryDate;
        FilePath = filePath;
        Status = DocumentStatus.Pending;
    }

    public void Verify()
    {
        Status = DocumentStatus.Verified;
        UpdateTimestamp();
    }
}

public enum DocumentStatus
{
    Pending = 1,
    Verified = 2,
    Expired = 3
}