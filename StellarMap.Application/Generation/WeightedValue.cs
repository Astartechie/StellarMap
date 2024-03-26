using StellarMap.Domain;

namespace StellarMap.Application.Generation;

public class WeightedValue<T> : ValueObject<WeightedValue<T>>
{
    public static WeightedValue<T> Create(T value, Weighting weight)
        => new(value, weight);

    protected WeightedValue(T value, Weighting weight)
    {
        Value = value;
        Weight = weight;
    }

    public T Value { get; }
    public Weighting Weight { get; }

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
        yield return Weight;
    }
}