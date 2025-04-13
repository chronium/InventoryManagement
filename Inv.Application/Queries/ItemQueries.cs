namespace Inv.Application.Queries;

public record GetAllItemsQuery;

public record ItemDto(Guid Id, string Name, string Sku);