using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using StellarMap.Application.Generation;
using StellarMap.Application.Generation.Galaxies;
using StellarMap.Domain.Galaxies;
using StellarMap.UserInterface.Console.Providing;
using StellarMap.UserInterface.Console.Providing.Galaxies;

namespace StellarMap.UserInterface.Console;

internal class Program
{
    private static void Main()
    {
        var serviceCollection = new ServiceCollection();

        serviceCollection.AddLogging(config => { config.AddConsole(c => c.TimestampFormat = "[HH:mm:ss] "); });

        var randomWrapper = new RandomWrapper(0);

        serviceCollection.AddSingleton<IGenerator<double>>(randomWrapper);

        serviceCollection.AddSingleton<IGenerator<Weighting>, WeightingGenerator>();

        serviceCollection.AddSingleton<IGenerator<StarId>, StarIdGenerator>();
        serviceCollection.AddSingleton<IGenerator<Name>, StarNameGenerator>();
        serviceCollection.AddSingleton<IGenerator<StarClassification>, StarClassificationGenerator>();
        serviceCollection.AddSingleton<IGenerator<StarSize, StarClassification>, StarSizeGenerator>();
        serviceCollection.AddSingleton<IGenerator<Star>, StarGenerator>();

        serviceCollection.AddSingleton<IReader<StarClassificationProvider>>(new FileWrapper<StarClassificationProvider>("Star Classifications.csv"));
        serviceCollection.AddSingleton<IProvider<WeightedList<StarClassification>>, StarClassificationProvider>();

        var serviceProvider = serviceCollection.BuildServiceProvider();
        var starGenerator = serviceProvider.GetService<IGenerator<Star>>();
        starGenerator.Generate();
    }
}