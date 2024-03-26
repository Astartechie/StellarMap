namespace StellarMap.Domain;

public abstract class SingleValueObject<T, TValue> : ValueObject<T> where T : ValueObject<T>
{
    protected SingleValueObject(TValue value)
    {
        Value = value;
    }

    public TValue Value { get; }

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }
}