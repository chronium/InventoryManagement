using Inv.Domain.Entities.IdTypes;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Inv.Infrastructure.ValueConverters;

public class ItemIdValueConverter() : ValueConverter<ItemId, Guid>(v => v.Value, v => new(v));
public class StockItemIdValueConverter() : ValueConverter<StockItemId, Guid>(v => v.Value, v => new(v));
public class WarehouseIdValueConverter() : ValueConverter<WarehouseId, Guid>(v => v.Value, v => new(v));
