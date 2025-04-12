using Inv.Domain.Entities.IdTypes;

namespace Inv.Domain.Entities;

public class StockItem
{
    public StockItem(WarehouseId warehouseId, ItemId itemId, int quantity)
    {
        WarehouseId = warehouseId;
        ItemId = itemId;
        Quantity = quantity;
    }

    public StockItemId Id { get; private set; }
    
    public WarehouseId WarehouseId { get; private set; }
    public ItemId ItemId { get; private set; }
    
    public int Quantity { get; private set; }
}