using StellarMap.Domain.Galaxies.Mapping;

namespace StellarMap.Domain.Galaxies.Planets;

public class PlanetClassification(PlanetClassificationId id, Name name, Colour colour) : Entity<PlanetClassificationId>(id)
{
    public Name Name { get; } = name;
    public Colour Colour { get; } = colour;
}