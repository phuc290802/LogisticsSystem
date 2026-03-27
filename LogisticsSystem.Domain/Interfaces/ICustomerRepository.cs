using LogisticsSystem.Domain.Entities;

namespace LogisticsSystem.Domain.Interfaces;

public interface ICustomerRepository : IRepository<Customer>
{
    Task<Customer?> GetByCodeAsync(string code, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Customer>> GetByTypeAsync(CustomerType type, CancellationToken cancellationToken = default);
    Task<bool> IsCodeExistsAsync(string code, int? excludeId = null, CancellationToken cancellationToken = default);
}