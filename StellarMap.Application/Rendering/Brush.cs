using StellarMap.Domain;
using StellarMap.Domain.Galaxies.Mapping;

namespace StellarMap.Application.Rendering;

public class Brush : ValueObject<Brush>
{
    public static Brush None = new(Colour.Defined.None);

    public static Brush Create(Colour colour)
        => new(colour);

    protected Brush(Colour colour)
    {
        Colour = colour;
    }

    public Colour Colour { get; }

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Colour;
    }
}