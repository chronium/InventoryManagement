namespace Inv.Domain.Entities.IdTypes;

public readonly struct WarehouseId(Guid value) : IEquatable<WarehouseId>
{
    public Guid Value { get; } = value;

    public static explicit operator Guid(WarehouseId id)
    {
        return id.Value;
    }

    public static explicit operator WarehouseId(Guid id)
    {
        return new(id);
    }

    public override string ToString()
    {
        return Value.ToString();
    }

    public bool Equals(WarehouseId other)
    {
        return Value.Equals(other.Value);
    }

    public override bool Equals(object? obj)
    {
        return obj is WarehouseId other && Equals(other);
    }

    public override int GetHashCode()
    {
        return Value.GetHashCode();
    }

    public static bool operator ==(WarehouseId left, WarehouseId right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(WarehouseId left, WarehouseId right)
    {
        return !(left == right);
    }
}