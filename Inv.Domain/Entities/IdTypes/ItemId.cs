namespace Inv.Domain.Entities.IdTypes;

public readonly struct ItemId(Guid value) : IEquatable<ItemId>
{
    public Guid Value { get; } = value;

    public static explicit operator Guid(ItemId id)
    {
        return id.Value;
    }

    public static explicit operator ItemId(Guid id)
    {
        return new(id);
    }

    public override string ToString()
    {
        return Value.ToString();
    }

    public override bool Equals(object? obj)
    {
        if (obj is ItemId other) return Value.Equals(other.Value);

        return false;
    }

    public bool Equals(ItemId other)
    {
        return Value.Equals(other.Value);
    }

    public override int GetHashCode()
    {
        return Value.GetHashCode();
    }

    public static bool operator ==(ItemId left, ItemId right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(ItemId left, ItemId right)
    {
        return !(left == right);
    }
}