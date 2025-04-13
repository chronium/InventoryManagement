using Inv.Domain.Entities;
using Inv.Domain.Entities.IdTypes;

namespace Inv.Application.Interfaces;

public interface IWarehouseRepository
{
    Task<Warehouse> GetByIdAsync(WarehouseId id, CancellationToken cancellationToken);
    Task<List<Warehouse>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(Warehouse warehouse, CancellationToken cancellationToken);
    Task SaveChangesAsync(CancellationToken cancellationToken);
}

public interface IItemRepository
{
    Task<Item> GetByIdAsync(ItemId id, CancellationToken cancellationToken);
    Task<List<Item>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(Item item, CancellationToken cancellationToken);
    Task SaveChangesAsync(CancellationToken cancellationToken);
}