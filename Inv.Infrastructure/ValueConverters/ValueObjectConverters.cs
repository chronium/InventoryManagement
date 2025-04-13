using Inv.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Inv.Infrastructure.ValueConverters;

public class ItemSkuValueConverter() : ValueConverter<ItemSku, string>(v => v.Value, v => new(v));

public class ItemNameValueConverter() : ValueConverter<ItemName, string>(v => v.Value, v => new(v));