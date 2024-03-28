using StellarMap.Domain.Galaxies.Stars;

namespace StellarMap.Application.Generation.Generators.Galaxies.Stars;

public class StarSizeGenerator(IRandomDoubleGenerator doubleGenerator) : IStarSizeGenerator
{
    public StarSize Generate(StarClassification input)
        => StarSize.Create(doubleGenerator.Generate() * (input.Sizes.Maximum.Value - input.Sizes.Minimum.Value) + input.Sizes.Minimum.Value);
}