using LogisticsSystem.Application.Common;
using LogisticsSystem.Application.DTOs;

namespace LogisticsSystem.Application.Features.Customers.Queries;

public class GetCustomerByIdQuery : IQuery<CustomerDto?>
{
    public int Id { get; set; }
}

public class GetAllCustomersQuery : IQuery<IReadOnlyList<CustomerDto>>
{
    public string? Type { get; set; }
    public bool? IsActive { get; set; }
}

public class GetCustomerByCodeQuery : IQuery<CustomerDto?>
{
    public string Code { get; set; } = string.Empty;
}