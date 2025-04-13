using Inv.Application.Interfaces;
using Inv.Domain.Entities;
using Inv.Domain.Entities.IdTypes;
using Microsoft.EntityFrameworkCore;

namespace Inv.Infrastructure.Repositories;

public class WarehouseRepository(WarehouseContext dbContext) : IWarehouseRepository
{
    public Task<Warehouse> GetByIdAsync(WarehouseId id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<List<Warehouse>> GetAllAsync(CancellationToken cancellationToken)
    {
        return dbContext.Warehouses
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public Task AddAsync(Warehouse warehouse, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task SaveChangesAsync(CancellationToken cancellationToken)
    {
        return dbContext.SaveChangesAsync(cancellationToken);
    }
}