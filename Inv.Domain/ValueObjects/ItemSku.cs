namespace Inv.Domain.ValueObjects;

public readonly struct ItemSku(string sku) : IEquatable<ItemSku>
{
    public ItemSku()
        : this(string.Empty)
    {
    }

    public string Value { get; } = sku;

    public static explicit operator string(ItemSku itemName)
    {
        return itemName.Value;
    }

    public static explicit operator ItemSku(string itemName)
    {
        return new(itemName);
    }

    public override string ToString()
    {
        return Value;
    }

    public override bool Equals(object? obj)
    {
        return obj is ItemSku other && Equals(other);
    }

    public bool Equals(ItemSku other)
    {
        return Value.Equals(other.Value);
    }

    public override int GetHashCode()
    {
        return Value.GetHashCode();
    }

    public static bool operator ==(ItemSku left, ItemSku right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(ItemSku left, ItemSku right)
    {
        return !(left == right);
    }
}