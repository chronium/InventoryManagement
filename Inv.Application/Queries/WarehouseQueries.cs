using Inv.Domain.Entities.IdTypes;

namespace Inv.Application.Queries;

public record GetAllWarehousesQuery;

public record GetWarehouseByIdQuery(WarehouseId Id);
