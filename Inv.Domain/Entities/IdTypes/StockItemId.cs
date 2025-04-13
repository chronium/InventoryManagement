namespace Inv.Domain.Entities.IdTypes;

public readonly struct StockItemId(Guid value) : IEquatable<StockItemId>
{
    public Guid Value { get; } = value;

    public static explicit operator Guid(StockItemId id)
    {
        return id.Value;
    }

    public static explicit operator StockItemId(Guid id)
    {
        return new(id);
    }

    public override string ToString()
    {
        return Value.ToString();
    }

    public bool Equals(StockItemId other)
    {
        return Value.Equals(other.Value);
    }

    public override bool Equals(object? obj)
    {
        return obj is StockItemId other && Equals(other);
    }

    public override int GetHashCode()
    {
        return Value.GetHashCode();
    }

    public static bool operator ==(StockItemId left, StockItemId right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(StockItemId left, StockItemId right)
    {
        return !(left == right);
    }
}