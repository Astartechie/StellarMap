using Microsoft.Extensions.Logging;
using StellarMap.Domain.Galaxies.Planets;

namespace StellarMap.Application.Generation.Generators.Galaxies.Planets;

public class PlanetGenerator(
    ILogger<PlanetGenerator> logger,
    IPlanetIdGenerator planetIdGenerator,
    IPlanetNameGenerator planetNameGenerator,
    IPlanetClassificationGenerator planetClassificationGenerator) : IPlanetGenerator
{
    public Planet Generate()
    {
        var id = planetIdGenerator.Generate();
        var name = planetNameGenerator.Generate();
        var classification = planetClassificationGenerator.Generate();

        logger.LogInformation("Generated Planet: '{name}' ({id}) - {classification}", name, id, classification.Name);
        return new Planet(id, name, classification);
    }
}