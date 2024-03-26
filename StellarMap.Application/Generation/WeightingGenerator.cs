namespace StellarMap.Application.Generation;

public class WeightingGenerator(IGenerator<double> doubleGenerator) : IGenerator<Weighting>
{
    public Weighting Generate()
    {
        return Weighting.Create(doubleGenerator.Generate());
    }
}