using Inv.Application.Interfaces;
using Inv.Domain.Entities;
using Inv.Domain.Entities.IdTypes;

namespace Inv.Infrastructure.Repositories;

public class ItemRepository(WarehouseContext dbContext) : IItemRepository
{
    public Task<Item> GetByIdAsync(ItemId id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<List<Item>> GetAllAsync(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task AddAsync(Item item, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task SaveChangesAsync(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}