using Inv.Domain.Entities.IdTypes;

namespace Inv.Domain.Entities;

public class Item
{
    public Item(string sku, string name)
    {
        SKU = sku;
        Name = name;
    }

    public ItemId Id { get; private set; }
    
    public string SKU { get; private set; }
    public string Name { get; private set; }
}