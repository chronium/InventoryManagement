namespace Inv.Application.Commands;

public record CreateWarehouseCommand(string Name);

public record AddStockCommand(Guid WarehouseId, Guid ItemId, int Quantity);

public record AddStockDto(Guid ItemId, int Quantity);

public record MoveStockCommand(Guid SourceWarehouseId, Guid DestinationWarehouseId, Guid ItemId, int Quantity);

public record MoveStockDto(Guid DestinationWarehouseId, Guid ItemId, int Quantity);