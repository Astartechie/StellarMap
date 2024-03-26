using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using StellarMap.Application.Generation;
using StellarMap.Application.Generation.Galaxies;
using StellarMap.Application.Generation.Galaxies.Mapping;
using StellarMap.Application.Rendering;
using StellarMap.Application.Rendering.Galaxies.Mapping;
using StellarMap.Domain.Galaxies;
using StellarMap.Domain.Galaxies.Mapping;
using StellarMap.UserInterface.Console.Providing;
using StellarMap.UserInterface.Console.Providing.Galaxies;
using StellarMap.UserInterface.Console.Rendering;
using StellarMap.UserInterface.Console.Rendering.ScalableVectorGraphics;

namespace StellarMap.UserInterface.Console;

internal class Program
{
    private static void Main()
    {
        var radius = 50;
        var hexSize = 20f;


        var serviceCollection = new ServiceCollection();

        serviceCollection.AddLogging(config => { config.AddConsole(c => c.TimestampFormat = "[HH:mm:ss] "); });

        var random = new Random(0);

        serviceCollection.AddSingleton<IGenerator<double>>(new RandomWrapper<double>(random, r => r.NextDouble()));
        serviceCollection.AddSingleton<IGenerator<Guid>>(new RandomWrapper<Guid>(random, r =>
        {
            var bytes = new byte[16];
            random.NextBytes(bytes);
            return new Guid(bytes);
        }));

        serviceCollection.AddSingleton<IGenerator<int, (int, int)>>(new RandomWrapper<int, (int, int)>(random, (r, range) => r.Next(range.Item1, range.Item2)));


        serviceCollection.AddSingleton<IGenerator<Weighting>, WeightingGenerator>();

        serviceCollection.AddSingleton<IGenerator<StarId>, StarIdGenerator>();
        serviceCollection.AddSingleton<IGenerator<Name>, StarNameGenerator>();
        serviceCollection.AddSingleton<IGenerator<StarClassification>, StarClassificationGenerator>();
        serviceCollection.AddSingleton<IGenerator<StarSize, StarClassification>, StarSizeGenerator>();
        serviceCollection.AddSingleton<IGenerator<Star>, StarGenerator>();

        serviceCollection.AddSingleton(new HexagonalGridGenerator.Settings(radius, hexSize));
        serviceCollection.AddSingleton<IGenerator<HexagonalGrid>, HexagonalGridGenerator>();

        serviceCollection.AddSingleton<IReader<StarClassificationProvider>>(new FileWrapper<StarClassificationProvider>("Star Classifications.csv"));
        serviceCollection.AddSingleton<IProvider<WeightedList<StarClassification>>, StarClassificationProvider>();

        serviceCollection.AddSingleton<IReader<StarNameProvider>>(new FileWrapper<StarNameProvider>("Star Names.txt"));
        serviceCollection.AddSingleton<IProvider<MarkovChain<char>>, StarNameProvider>();

        serviceCollection.AddSingleton<IWriter>(new TextFileWriter("test.svg"));
        serviceCollection.AddSingleton<IRenderTarget, RenderTarget>();
        serviceCollection.AddSingleton(new HexagonalGridRenderer.Settings((int)hexSize * 4 * radius, (int)hexSize * 4 * radius, hexSize));
        serviceCollection.AddSingleton<IRenderer<HexagonalGrid>, HexagonalGridRenderer>();

        var serviceProvider = serviceCollection.BuildServiceProvider();
        var generator = serviceProvider.GetService<IGenerator<HexagonalGrid>>();
        var grid = generator.Generate();

        var gridRenderer = serviceProvider.GetService<IRenderer<HexagonalGrid>>();
        gridRenderer.Render(grid);
    }
}