using Inv.Domain.Entities.IdTypes;

namespace Inv.Domain.Entities;

public class Warehouse
{
    public Warehouse(string name)
    {
        Name = name;
    }
    
    public WarehouseId Id { get; private set; }
    
    public string Name { get; private set; }
}