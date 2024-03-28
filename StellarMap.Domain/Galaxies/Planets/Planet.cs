namespace StellarMap.Domain.Galaxies.Planets;

public class Planet(PlanetId id, Name name, PlanetClassification classification) : Entity<PlanetId>(id)
{
    public Name Name { get; } = name;
    public PlanetClassification Classification { get; } = classification;
}