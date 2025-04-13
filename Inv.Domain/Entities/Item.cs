using Inv.Domain.Entities.IdTypes;

namespace Inv.Domain.Entities;

public class Item(string sku, string name)
{
    public ItemId Id { get; private set; }

    public string Sku { get; private set; } = sku;
    public string Name { get; private set; } = name;
}