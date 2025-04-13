using Inv.Domain.Entities.IdTypes;
using Inv.Domain.ValueObjects;

namespace Inv.Domain.Entities;

public class Item(ItemSku sku, ItemName name)
{
    public ItemId Id { get; private set; }

    public ItemSku Sku { get; private set; } = sku;
    public ItemName Name { get; private set; } = name;
}