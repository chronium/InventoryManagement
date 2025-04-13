using Inv.Application.Interfaces;
using Inv.Domain.Entities;
using Inv.Domain.Entities.IdTypes;

namespace Inv.Infrastructure.Repositories;

public class WarehouseRepository(WarehouseContext dbContext) : IWarehouseRepository
{
    public Task<Warehouse> GetByIdAsync(WarehouseId id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<List<Warehouse>> GetAllAsync(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task AddAsync(Warehouse warehouse, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task SaveChangesAsync(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}