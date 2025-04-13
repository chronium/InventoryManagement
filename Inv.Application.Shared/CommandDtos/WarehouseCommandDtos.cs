namespace Inv.Application.Shared.CommandDtos;

public record CreateWarehouseDto(string Name);

public record AddStockDto(Guid ItemId, int Quantity);

public record MoveStockDto(Guid DestinationWarehouseId, Guid ItemId, int Quantity);