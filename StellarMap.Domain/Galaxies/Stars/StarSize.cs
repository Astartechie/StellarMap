namespace StellarMap.Domain.Galaxies.Stars;

public class StarSize : SingleValueObject<StarSize, double>
{
    private const double MinimumValue = 0;
    private const double MaximumValue = 8;

    public static readonly StarSize Minimum = new(MinimumValue);
    public static readonly StarSize Maximum = new(MaximumValue);
    public static readonly Range<StarSize> Range = Range<StarSize>.Create(Minimum, Maximum);

    public static StarSize Create(double value)
        => value < MinimumValue
            ? throw new ArgumentOutOfRangeException(nameof(value))
            : new StarSize(value);

    protected StarSize(double value) : base(value)
    {
    }
}