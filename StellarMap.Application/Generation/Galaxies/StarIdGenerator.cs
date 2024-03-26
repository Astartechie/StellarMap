using StellarMap.Domain.Galaxies;

namespace StellarMap.Application.Generation.Galaxies;

public class StarIdGenerator(IGenerator<Guid> guidGenerator) : IStarIdGenerator
{
    public StarId Generate()
        => StarId.Create(guidGenerator.Generate());
}