using LogisticsSystem.Domain.Entities;
using LogisticsSystem.Domain.Interfaces;
using LogisticsSystem.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace LogisticsSystem.Infrastructure.Repositories;

public class CustomerRepository : GenericRepository<Customer>, ICustomerRepository
{
    public CustomerRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<Customer?> GetByCodeAsync(string code, CancellationToken cancellationToken = default)
    {
        return await _dbSet.FirstOrDefaultAsync(c => c.Code == code, cancellationToken);
    }

    public async Task<IReadOnlyList<Customer>> GetByTypeAsync(CustomerType type, CancellationToken cancellationToken = default)
    {
        return await _dbSet.Where(c => c.Type == type).ToListAsync(cancellationToken);
    }

    public async Task<bool> IsCodeExistsAsync(string code, int? excludeId = null, CancellationToken cancellationToken = default)
    {
        var query = _dbSet.Where(c => c.Code == code);

        if (excludeId.HasValue)
        {
            query = query.Where(c => c.Id != excludeId.Value);
        }

        return await query.AnyAsync(cancellationToken);
    }
}