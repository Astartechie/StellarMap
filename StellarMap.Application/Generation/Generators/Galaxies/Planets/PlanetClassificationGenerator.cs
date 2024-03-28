using StellarMap.Domain.Galaxies.Planets;

namespace StellarMap.Application.Generation.Generators.Galaxies.Planets;

public class PlanetClassificationGenerator(
    IGenerator<Weighting> weightingGenerator,
    IProvider<WeightedList<PlanetClassification>> planetClassificationProvider
) : IPlanetClassificationGenerator
{
    public PlanetClassification Generate()
    {
        var planetClassifications = planetClassificationProvider.Provide();
        return planetClassifications.GetItemFromWeight(weightingGenerator.Generate());
    }
}