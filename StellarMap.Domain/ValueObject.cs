namespace StellarMap.Domain;

public abstract class ValueObject<T> : IEquatable<T> where T : ValueObject<T>, IEquatable<T>
{
    public bool Equals(T? other)
        => other is not null && GetEqualityComponents().SequenceEqual(other.GetEqualityComponents());

    public override bool Equals(object? obj)
        => Equals(obj as T);

    public override int GetHashCode()
        => GetEqualityComponents().Select(x => x?.GetHashCode() ?? 0).Aggregate((x, y) => x ^ y);

    public override string ToString()
        => string.Join(", ", GetEqualityComponents());

    public static bool operator ==(ValueObject<T>? left, ValueObject<T> right)
        => Equals(left, right);

    public static bool operator !=(ValueObject<T> left, ValueObject<T> right)
        => !Equals(left, right);

    protected abstract IEnumerable<object?> GetEqualityComponents();
}