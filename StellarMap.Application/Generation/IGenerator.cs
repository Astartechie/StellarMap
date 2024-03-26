namespace StellarMap.Application.Generation;

public interface IGenerator<out TOut>
{
    TOut Generate();
}

public interface IGenerator<out TOut, in TIn>
{
    TOut Generate(TIn input);
}