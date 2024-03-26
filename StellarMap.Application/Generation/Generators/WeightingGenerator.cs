namespace StellarMap.Application.Generation.Generators;

public class WeightingGenerator(IRandomDoubleGenerator doubleGenerator) : IGenerator<Weighting>
{
    public Weighting Generate()
    {
        return Weighting.Create(doubleGenerator.Generate());
    }
}