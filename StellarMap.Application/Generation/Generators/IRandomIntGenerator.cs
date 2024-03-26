namespace StellarMap.Application.Generation.Generators;

public interface IRandomIntGenerator : IGenerator<int>, IGenerator<int, (int, int)>
{
}