namespace StellarMap.Domain.Galaxies;

public class Range<T> : ValueObject<Range<T>>
{
    public static Range<T> Create(T minimum, T maximum)
        => new(minimum, maximum);

    protected Range(T minimum, T maximum)
    {
        Minimum = minimum;
        Maximum = maximum;
    }

    public T Minimum { get; }
    public T Maximum { get; }

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Minimum;
        yield return Maximum;
    }
}