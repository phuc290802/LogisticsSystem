using LogisticsSystem.Domain.Common;
using LogisticsSystem.Domain.Entities;

namespace LogisticsSystem.Domain.Events;

public class CustomerCreatedEvent : DomainEvent
{
    public Customer Customer { get; }

    public CustomerCreatedEvent(Customer customer)
    {
        Customer = customer;
    }
}

public class CustomerUpdatedEvent : DomainEvent
{
    public Customer Customer { get; }

    public CustomerUpdatedEvent(Customer customer)
    {
        Customer = customer;
    }
}