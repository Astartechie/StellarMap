using Microsoft.Extensions.DependencyInjection;
using StellarMap.Application.Generation.Generators.Galaxies.Mapping;
using StellarMap.Application.Generation.Generators.Galaxies.Planets;
using StellarMap.Application.Generation.Generators.Galaxies.Stars;

namespace StellarMap.Application;

public static class ServiceCollectionExtensions
{
    public static void AddApplication(this IServiceCollection serviceCollection)
    {
        //Star
        serviceCollection.AddSingleton<IStarIdGenerator, StarIdGenerator>();
        serviceCollection.AddSingleton<IStarNameGenerator, StarNameGenerator>();
        serviceCollection.AddSingleton<IStarClassificationGenerator, StarClassificationGenerator>();
        serviceCollection.AddSingleton<IStarSizeGenerator, StarSizeGenerator>();
        serviceCollection.AddSingleton<IStarGenerator, StarGenerator>();

        //Planet
        serviceCollection.AddSingleton<IPlanetIdGenerator, PlanetIdGenerator>();
        serviceCollection.AddSingleton<IPlanetNameGenerator, PlanetNameGenerator>();
        serviceCollection.AddSingleton<IPlanetClassificationGenerator, PlanetClassificationGenerator>();
        serviceCollection.AddSingleton<IPlanetGenerator, PlanetGenerator>();

        //Hexagonal Grid
        serviceCollection.AddSingleton<IHexagonalGridGenerator, HexagonalGridGenerator>();
    }
}