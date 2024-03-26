using StellarMap.Application.Generation.Generators;
using StellarMap.Domain.Galaxies;

namespace StellarMap.Application.Generation.Generators.Galaxies;

public class StarIdGenerator(IRandomGuidGenerator guidGenerator) : IStarIdGenerator
{
    public StarId Generate()
        => StarId.Create(guidGenerator.Generate());
}