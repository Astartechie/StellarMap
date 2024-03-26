namespace StellarMap.Application.Generation.Generators;

public interface IGenerator<out TOut>
{
    TOut Generate();
}

public interface IGenerator<out TOut, in TIn>
{
    TOut Generate(TIn input);
}