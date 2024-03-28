using StellarMap.Domain.Galaxies.Mapping;

namespace StellarMap.Domain.Galaxies.Stars;

public class StarClassification(StarClassificationId id, Colour colour, Range<StarSize> sizes) : Entity<StarClassificationId>(id)
{
    public Colour Colour { get; } = colour;
    public Range<StarSize> Sizes { get; } = sizes;
}