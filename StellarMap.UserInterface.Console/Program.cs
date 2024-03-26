using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using StellarMap.Application.Generation;
using StellarMap.Application.Generation.Generators;
using StellarMap.Application.Generation.Generators.Galaxies;
using StellarMap.Application.Generation.Generators.Galaxies.Mapping;
using StellarMap.Application.Rendering;
using StellarMap.Application.Rendering.Galaxies.Mapping;
using StellarMap.Domain.Galaxies;
using StellarMap.Domain.Galaxies.Mapping;
using StellarMap.UserInterface.Console.Generation.Generators;
using StellarMap.UserInterface.Console.Providing;
using StellarMap.UserInterface.Console.Providing.Galaxies;
using StellarMap.UserInterface.Console.Rendering;
using StellarMap.UserInterface.Console.Rendering.ScalableVectorGraphics;

namespace StellarMap.UserInterface.Console;

internal class Program(IHexagonalGridGenerator hexagonalGridGenerator, IRenderer<HexagonalGrid> hexagonalGridRenderer)
{
    private static void Main()
    {
        var radius = 50;
        var hexSize = 20f;

        var serviceCollection = new ServiceCollection();

        serviceCollection.AddLogging(config => { config.AddConsole(c => c.TimestampFormat = "[HH:mm:ss] "); });

        //Randomness
        var random = new Random(0);
        serviceCollection.AddSingleton(random);
        serviceCollection.AddSingleton<IRandomDoubleGenerator, RandomDoubleGenerator>();
        serviceCollection.AddSingleton<IRandomGuidGenerator, RandomGuidGenerator>();
        serviceCollection.AddSingleton<IRandomIntGenerator, RandomIntGenerator>();

        serviceCollection.AddSingleton<IGenerator<Weighting>, WeightingGenerator>();

        //Star Generation
        serviceCollection.AddSingleton<IStarIdGenerator, StarIdGenerator>();
        serviceCollection.AddSingleton<IStarNameGenerator, StarNameGenerator>();
        serviceCollection.AddSingleton<IStarClassificationGenerator, StarClassificationGenerator>();
        serviceCollection.AddSingleton<IStarSizeGenerator, StarSizeGenerator>();
        serviceCollection.AddSingleton<IStarGenerator, StarGenerator>();

        //Hexagonal Grid Generation
        serviceCollection.AddSingleton(new StellarNoise.Settings(radius, hexSize));
        serviceCollection.AddSingleton<IStellarNoise, StellarNoise>();

        serviceCollection.AddSingleton(new HexagonalGridGenerator.Settings(radius));
        serviceCollection.AddSingleton<IHexagonalGridGenerator, HexagonalGridGenerator>();

        //Providers
        serviceCollection.AddSingleton<IReader<StarClassificationProvider>>(new FileWrapper<StarClassificationProvider>("Star Classifications.csv"));
        serviceCollection.AddSingleton<IProvider<WeightedList<StarClassification>>, StarClassificationProvider>();

        serviceCollection.AddSingleton<IWriter>(new TextFileWriter("test.svg"));
        serviceCollection.AddSingleton<IRenderTarget, RenderTarget>();
        serviceCollection.AddSingleton(new HexagonalGridRenderer.Settings((int)hexSize * 4 * radius, (int)hexSize * 4 * radius, hexSize));
        serviceCollection.AddSingleton<IRenderer<HexagonalGrid>, HexagonalGridRenderer>();

        serviceCollection.AddSingleton<Program>();
        var serviceProvider = serviceCollection.BuildServiceProvider();
        var program = serviceProvider.GetService<Program>();
        program.Run();
    }

    public void Run()
    {
        var grid = hexagonalGridGenerator.Generate();
        hexagonalGridRenderer.Render(grid);
    }
}