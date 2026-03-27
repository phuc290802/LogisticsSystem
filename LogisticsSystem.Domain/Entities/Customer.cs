using LogisticsSystem.Domain.Common;
using LogisticsSystem.Domain.Events;

namespace LogisticsSystem.Domain.Entities;

public class Customer : BaseEntity, IAggregateRoot
{
    private readonly List<IDomainEvent> _domainEvents = new();

    public string Code { get; private set; }
    public string Name { get; private set; }
    public string? TaxCode { get; private set; }
    public string? Address { get; private set; }
    public string? Phone { get; private set; }
    public string? Email { get; private set; }
    public CustomerType Type { get; private set; }
    public bool IsActive { get; private set; }

    public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

    private Customer() { } // EF Core

    public Customer(string code, string name, CustomerType type, string? taxCode = null)
    {
        if (string.IsNullOrWhiteSpace(code))
            throw new ArgumentException("Customer code is required", nameof(code));

        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Customer name is required", nameof(name));

        Code = code;
        Name = name;
        Type = type;
        TaxCode = taxCode;
        IsActive = true;

        AddDomainEvent(new CustomerCreatedEvent(this));
    }

    public void UpdateInfo(string name, string? address, string? phone, string? email)
    {
        Name = name;
        Address = address;
        Phone = phone;
        Email = email;
        UpdateTimestamp();

        AddDomainEvent(new CustomerUpdatedEvent(this));
    }

    public void UpdateType(CustomerType type)
    {
        Type = type;
        UpdateTimestamp();
    }

    public void Activate()
    {
        IsActive = true;
        UpdateTimestamp();
    }

    public void Deactivate()
    {
        IsActive = false;
        UpdateTimestamp();
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