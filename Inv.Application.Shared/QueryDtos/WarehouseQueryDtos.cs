namespace Inv.Application.Shared.QueryDtos;

public record GetWarehouseByIdDto(Guid Id);
public record WarehouseDto(Guid Id, string Name);

public record InventoryItemDto(Guid Id, string Sku, string Name, int Quantity);
public record WarehouseWithInventoryDto(Guid Id, string Name, List<InventoryItemDto> Inventory);
