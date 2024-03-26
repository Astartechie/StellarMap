using StellarMap.Application.Generation;

namespace StellarMap.UserInterface.Console;

internal class RandomWrapper(int seed) : IGenerator<double>
{
    public double Generate()
        => _random.NextDouble();

    private readonly Random _random = new(seed);
}