using StellarMap.Domain;
using StellarMap.Domain.Galaxies.Mapping;

namespace StellarMap.Application.Rendering;

public class Pen : ValueObject<Pen>
{
    private const float MinimumThickness = 0;

    public static readonly Pen None = new(0, Colour.Defined.None);

    public static Pen Create(float thickness, Colour colour)
        => thickness < MinimumThickness
            ? throw new ArgumentOutOfRangeException(nameof(thickness))
            : new Pen(thickness, colour);

    protected Pen(float thickness, Colour colour)
    {
        Thickness = thickness;
        Colour = colour;
    }

    public float Thickness { get; }

    public Colour Colour { get; }


    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Thickness;
        yield return Colour;
    }
}