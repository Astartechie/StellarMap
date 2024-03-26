using StellarMap.Application.Generation.Generators;

namespace StellarMap.UserInterface.Console.Generation.Generators;

internal class RandomDoubleGenerator(Random random) : IRandomDoubleGenerator
{
    public double Generate()
        => random.NextDouble();
}