using StellarMap.Domain.Galaxies.Mapping;

namespace StellarMap.Application.Generation.Galaxies;

public interface IStellarNoise
{
    int StarCount(Coordinate position);
}