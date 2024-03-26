namespace StellarMap.Application.Generation;

public interface IGenerator<out T>
{
    T Generate();
}

public interface IGenerator<out TOut, in TIn>
{
    TOut Generate(TIn input);
}