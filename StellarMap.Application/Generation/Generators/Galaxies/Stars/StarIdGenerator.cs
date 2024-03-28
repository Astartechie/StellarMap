using StellarMap.Domain.Galaxies.Stars;

namespace StellarMap.Application.Generation.Generators.Galaxies.Stars;

public class StarIdGenerator(IRandomGuidGenerator guidGenerator) : IStarIdGenerator
{
    public StarId Generate()
        => StarId.Create(guidGenerator.Generate());
}