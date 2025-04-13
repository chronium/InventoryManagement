namespace Inv.Domain.ValueObjects;

public readonly struct ItemName(string name) : IEquatable<ItemName>
{
    public ItemName()
        : this(string.Empty)
    {
    }

    public string Value { get; } = name;

    public static explicit operator string(ItemName itemName)
    {
        return itemName.Value;
    }

    public static explicit operator ItemName(string itemName)
    {
        return new(itemName);
    }

    public override string ToString()
    {
        return Value;
    }

    public override bool Equals(object? obj)
    {
        return obj is ItemName other && Equals(other);
    }

    public bool Equals(ItemName other)
    {
        return Value.Equals(other.Value);
    }

    public override int GetHashCode()
    {
        return Value.GetHashCode();
    }

    public static bool operator ==(ItemName left, ItemName right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(ItemName left, ItemName right)
    {
        return !(left == right);
    }
}