using StellarMap.Domain;

namespace StellarMap.Application.Generation;

public class Weighting : SingleValueObject<Weighting, double>
{
    private const double MinimumValue = 0;
    public static readonly Weighting Minimum = new(MinimumValue);

    public static Weighting Create(double value)
        => value < MinimumValue
            ? throw new ArgumentOutOfRangeException(nameof(value))
            : new Weighting(value);


    protected Weighting(double value) : base(value)
    {
    }
}