using LogisticsSystem.Application.Common;
using LogisticsSystem.Application.DTOs;

namespace LogisticsSystem.Application.Features.Customers.Commands;

public class CreateCustomerCommand : ICommand<CustomerDto>
{
    public string Code { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string? TaxCode { get; set; }
    public string? Address { get; set; }
    public string? Phone { get; set; }
    public string? Email { get; set; }
    public string Type { get; set; } = string.Empty;
}

public class UpdateCustomerCommand : ICommand<CustomerDto>
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Address { get; set; }
    public string? Phone { get; set; }
    public string? Email { get; set; }
}

public class DeleteCustomerCommand : ICommand<bool>
{
    public int Id { get; set; }
}

public class ActivateCustomerCommand : ICommand<bool>
{
    public int Id { get; set; }
}

public class DeactivateCustomerCommand : ICommand<bool>
{
    public int Id { get; set; }
}