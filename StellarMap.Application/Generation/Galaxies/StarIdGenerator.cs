using StellarMap.Domain.Galaxies;

namespace StellarMap.Application.Generation.Galaxies;

public class StarIdGenerator(IGenerator<Guid> guidGenerator) : IGenerator<StarId>
{
    public StarId Generate()
        => StarId.Create(guidGenerator.Generate());
}