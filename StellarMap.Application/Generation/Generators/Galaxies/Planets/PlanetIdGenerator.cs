using StellarMap.Domain.Galaxies.Planets;

namespace StellarMap.Application.Generation.Generators.Galaxies.Planets;

public class PlanetIdGenerator(IRandomGuidGenerator guidGenerator) : IPlanetIdGenerator
{
    public PlanetId Generate()
        => PlanetId.Create(guidGenerator.Generate());
}