using Inv.Application.Shared.QueryDtos;

namespace Inv.UI.Models;

public readonly struct MoveStockModel(Guid sourceWarehouseId, InventoryItemDto item)
{
    public Guid SourceWarehouseId { get; } = sourceWarehouseId;
    public InventoryItemDto Item { get; } = item;
}