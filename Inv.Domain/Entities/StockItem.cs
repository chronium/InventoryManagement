using Inv.Domain.Entities.IdTypes;
using Inv.Domain.ValueObjects;

namespace Inv.Domain.Entities;

public class StockItem(WarehouseId warehouseId, ItemId itemId, ItemInfo itemInfo, int quantity)
{
    public StockItem(WarehouseId warehouseId, ItemId itemId, int quantity)
        : this(warehouseId, itemId, new(), quantity)
    {
    }

    public StockItemId Id { get; private set; }

    public WarehouseId WarehouseId { get; private set; } = warehouseId;
    public ItemId ItemId { get; private set; } = itemId;

    public int Quantity { get; private set; } = quantity;

    public ItemInfo ItemInfo { get; private set; } = itemInfo;

    public void Increase(int quantity)
    {
        Quantity += quantity;
    }

    public void Decrease(int quantity)
    {
        if (Quantity < quantity)
            throw new InvalidOperationException("Not enough stock to decrease.");

        Quantity -= quantity;
    }
}

public class ItemInfo(ItemSku sku, ItemName name)
{
    public ItemInfo()
        : this(new(), new())
    {
    }

    public ItemSku Sku { get; private set; } = sku;
    public ItemName Name { get; private set; } = name;
}