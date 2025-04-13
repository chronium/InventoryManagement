using Inv.Domain.Entities.IdTypes;

namespace Inv.Domain.Entities;

public class Warehouse(string name)
{
    private readonly List<StockItem> _inventory = [];

    public WarehouseId Id { get; private set; }

    public string Name { get; private set; } = name;
    public IReadOnlyCollection<StockItem> Inventory => _inventory.AsReadOnly();
}