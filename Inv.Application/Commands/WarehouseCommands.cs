using Inv.Domain.Entities.IdTypes;

namespace Inv.Application.Commands;

public record CreateWarehouseCommand(string Name);

public record AddStockCommand(WarehouseId WarehouseId, ItemId ItemId, int Quantity);

public record MoveStockCommand(
    WarehouseId SourceWarehouseId,
    WarehouseId DestinationWarehouseId,
    ItemId ItemId,
    int Quantity);