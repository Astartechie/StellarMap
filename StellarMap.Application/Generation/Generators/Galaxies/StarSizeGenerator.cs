using StellarMap.Application.Generation.Generators;
using StellarMap.Domain.Galaxies;

namespace StellarMap.Application.Generation.Generators.Galaxies;

public class StarSizeGenerator(IRandomDoubleGenerator doubleGenerator) : IStarSizeGenerator
{
    public StarSize Generate(StarClassification input)
        => StarSize.Create(doubleGenerator.Generate() * (input.Sizes.Maximum.Value - input.Sizes.Minimum.Value) + input.Sizes.Minimum.Value);
}