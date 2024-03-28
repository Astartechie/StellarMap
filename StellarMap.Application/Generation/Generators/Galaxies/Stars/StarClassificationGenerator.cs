using StellarMap.Domain.Galaxies.Stars;

namespace StellarMap.Application.Generation.Generators.Galaxies.Stars;

public class StarClassificationGenerator(
    IGenerator<Weighting> weightingGenerator,
    IProvider<WeightedList<StarClassification>> starClassificationProvider
) : IStarClassificationGenerator
{
    public StarClassification Generate()
    {
        var starClassifications = starClassificationProvider.Provide();
        return starClassifications.GetItemFromWeight(weightingGenerator.Generate());
    }
}