using Inv.Application.Interfaces;
using Inv.Domain.Entities;
using Inv.Domain.Entities.IdTypes;
using Microsoft.EntityFrameworkCore;

namespace Inv.Infrastructure.Repositories;

public class WarehouseRepository(WarehouseContext dbContext) : IWarehouseRepository
{
    public async Task<Warehouse?> GetByIdAsync(WarehouseId id, CancellationToken cancellationToken)
    {
        return await dbContext.Warehouses
            .Include(w => w.Inventory)
            .FirstOrDefaultAsync(w => w.Id == id, cancellationToken);
    }

    public Task<List<Warehouse>> GetAllAsync(CancellationToken cancellationToken)
    {
        return dbContext.Warehouses
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public Task AddAsync(Warehouse warehouse, CancellationToken cancellationToken)
    {
        dbContext.Warehouses.Add(warehouse);
        return Task.CompletedTask;
    }

    public Task SaveChangesAsync(CancellationToken cancellationToken)
    {
        return dbContext.SaveChangesAsync(cancellationToken);
    }
}