using StellarMap.Domain.Galaxies;

namespace StellarMap.Application.Generation.Galaxies;

public class StarSizeGenerator(IGenerator<double> doubleGenerator) : IGenerator<StarSize, StarClassification>
{
    public StarSize Generate(StarClassification input)
        => StarSize.Create(doubleGenerator.Generate() * (input.Sizes.Maximum.Value - input.Sizes.Minimum.Value) + input.Sizes.Minimum.Value);
}