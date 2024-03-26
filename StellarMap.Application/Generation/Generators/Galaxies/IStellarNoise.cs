using StellarMap.Domain.Galaxies.Mapping;

namespace StellarMap.Application.Generation.Generators.Galaxies;

public interface IStellarNoise
{
    int StarCount(Coordinate position);
}