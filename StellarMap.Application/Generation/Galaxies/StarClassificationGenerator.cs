﻿using StellarMap.Domain.Galaxies;

namespace StellarMap.Application.Generation.Galaxies;

public class StarClassificationGenerator(
    IGenerator<Weighting> weightingGenerator,
    IProvider<WeightedList<StarClassification>> starClassificationProvider
) : IGenerator<StarClassification>
{
    public StarClassification Generate()
    {
        var starClassifications = starClassificationProvider.Provide();
        return starClassifications.GetItemFromWeight(weightingGenerator.Generate());
    }
}