namespace Inv.Domain.Entities.IdTypes;

public readonly struct ItemId
{
    public ItemId(Guid value)
    {
        Value = value;
    }

    public Guid Value { get; }

    public static explicit operator Guid(ItemId id) => id.Value;

    public static explicit operator ItemId(Guid id) => new(id);

    public override string ToString() => Value.ToString();
}