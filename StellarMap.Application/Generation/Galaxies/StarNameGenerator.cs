using StellarMap.Domain.Galaxies;

namespace StellarMap.Application.Generation.Galaxies;

public class StarNameGenerator : IGenerator<Name>
{
    public Name Generate()
    {
        return Name.Create("Test");
    }
}