using StellarMap.Domain.Galaxies;

namespace StellarMap.Application.Generation.Galaxies;

public class StarSizeGenerator : IGenerator<StarSize, StarClassification>
{
    public StarSize Generate(StarClassification input)
    {
        return StarSize.Minimum;
    }
}