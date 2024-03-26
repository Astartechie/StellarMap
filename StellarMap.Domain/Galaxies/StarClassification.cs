using StellarMap.Domain.Galaxies.Mapping;

namespace StellarMap.Domain.Galaxies;

public class StarClassification(StarClassificationId id, Colour colour, Range<float> sizes) : Entity<StarClassificationId>(id)
{
    public Colour Colour { get; } = colour;
    public Range<float> Sizes { get; } = sizes;
}