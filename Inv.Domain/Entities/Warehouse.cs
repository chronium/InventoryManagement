using Inv.Domain.Entities.IdTypes;

namespace Inv.Domain.Entities;

public class Warehouse(string name)
{
    private readonly List<StockItem> _inventory = [];
    public IReadOnlyCollection<StockItem> Inventory => _inventory;

    public WarehouseId Id { get; private set; }

    public string Name { get; private set; } = name;

    public void AddStock(ItemId itemId, ItemInfo info, int quantity)
    {
        var existingStockItem = _inventory.FirstOrDefault(s => s.ItemId == itemId);
        if (existingStockItem is not null)
            existingStockItem.Increase(quantity);
        else
            _inventory.Add(new(Id, itemId, info, quantity));
    }

    public void RemoveStock(ItemId itemId, int quantity)
    {
        var existingStockItem = _inventory.FirstOrDefault(s => s.ItemId == itemId);
        if (existingStockItem is null || existingStockItem.Quantity < quantity)
            throw new InvalidOperationException("Not enough stock to remove.");

        existingStockItem.Decrease(quantity);
    }
}