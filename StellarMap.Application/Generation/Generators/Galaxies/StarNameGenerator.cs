using StellarMap.Domain.Galaxies;

namespace StellarMap.Application.Generation.Generators.Galaxies;

public class StarNameGenerator(MarkovNameGenerator markovNameGenerator) : IStarNameGenerator
{
    public Name Generate()
    {
        return Name.Create(markovNameGenerator.GenerateName(3, 10));
    }
}