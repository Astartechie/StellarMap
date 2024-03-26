using StellarMap.Application.Generation.Generators;

namespace StellarMap.UserInterface.Console.Generation.Generators;

public class RandomIntGenerator(Random random) : IRandomIntGenerator
{
    public int Generate()
        => random.Next();

    public int Generate((int, int) input)
        => random.Next(input.Item1, input.Item2);
}