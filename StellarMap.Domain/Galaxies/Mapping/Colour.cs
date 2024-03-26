namespace StellarMap.Domain.Galaxies.Mapping;

public class Colour : ValueObject<Colour>
{
    private const byte DefaultAlpha = 255;

    public static readonly Colour Black = Create(0, 0, 0);

    public static Colour Create(byte red, byte green, byte blue, byte alpha = DefaultAlpha)
        => new(red, green, blue, alpha);

    protected Colour(byte red, byte green, byte blue, byte alpha)
    {
        Red = red;
        Green = green;
        Blue = blue;
        Alpha = alpha;
    }

    public byte Red { get; }
    public byte Green { get; }
    public byte Blue { get; }
    public byte Alpha { get; }

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Red;
        yield return Green;
        yield return Blue;
        yield return Alpha;
    }
}