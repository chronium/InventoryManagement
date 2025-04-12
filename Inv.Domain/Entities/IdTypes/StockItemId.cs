namespace Inv.Domain.Entities.IdTypes;

public readonly struct StockItemId
{
    public StockItemId(Guid value)
    {
        Value = value;
    }

    public Guid Value { get; }

    public static explicit operator Guid(StockItemId id) => id.Value;

    public static explicit operator StockItemId(Guid id) => new(id);

    public override string ToString() => Value.ToString();
}