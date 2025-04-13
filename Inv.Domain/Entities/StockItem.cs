using Inv.Domain.Entities.IdTypes;

namespace Inv.Domain.Entities;

public class StockItem(WarehouseId warehouseId, ItemId itemId, ItemInfo itemInfo, int quantity)
{
    public StockItemId Id { get; private set; }

    public WarehouseId WarehouseId { get; private set; } = warehouseId;
    public ItemId ItemId { get; private set; } = itemId;

    public int Quantity { get; private set; } = quantity;

    public ItemInfo ItemInfo { get; private set; } = itemInfo;
}

public class ItemInfo
{
    public ItemInfo(string sku, string name)
    {
        Sku = sku;
        Name = name;
    }

    public string Sku { get; private set; }
    public string Name { get; private set; }
}