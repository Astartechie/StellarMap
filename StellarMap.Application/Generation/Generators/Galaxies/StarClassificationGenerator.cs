using StellarMap.Application.Generation.Generators;
using StellarMap.Domain.Galaxies;

namespace StellarMap.Application.Generation.Generators.Galaxies;

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