using StellarMap.Domain.Galaxies;

namespace StellarMap.Application.Generation.Generators.Galaxies.Planets;

public class PlanetNameGenerator : IPlanetNameGenerator
{
    public Name Generate()
        => Name.Create("Test");
}