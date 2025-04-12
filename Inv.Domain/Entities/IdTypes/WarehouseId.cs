namespace Inv.Domain.Entities.IdTypes;

public readonly struct WarehouseId
{
    public WarehouseId(Guid value)
    {
        Value = value;
    }

    public Guid Value { get; }

    public static explicit operator Guid(WarehouseId id) => id.Value;

    public static explicit operator WarehouseId(Guid id) => new(id);

    public override string ToString() => Value.ToString();
}