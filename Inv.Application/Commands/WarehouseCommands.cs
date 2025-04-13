using Inv.Domain.Entities.IdTypes;

namespace Inv.Application.Commands;

public record CreateWarehouseCommand(string Name);

public record AddStockCommand(Guid WarehouseId, Guid ItemId, int Quantity);

public record AddStockDto(Guid ItemId, int Quantity);