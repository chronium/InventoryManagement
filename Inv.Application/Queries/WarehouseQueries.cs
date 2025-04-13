using Inv.Domain.Entities.IdTypes;

namespace Inv.Application.Queries;

public record GetAllWarehousesQuery;

public record GetWarehouseByIdQuery(WarehouseId Id);

public record WarehouseDto(Guid Id, string Name);

public record InventoryItemDto(Guid Id, string Sku, string Name, int Quantity);

public record WarehouseWithInventoryDto(Guid Id, string Name, List<InventoryItemDto> Inventory);