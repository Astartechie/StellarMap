using StellarMap.Application.Generation;

namespace StellarMap.UserInterface.Console;

internal class RandomWrapper<TOut>(Random random, Func<Random, TOut> function) : IGenerator<TOut>
{
    public TOut Generate()
        => function.Invoke(random);
}

internal class RandomWrapper<TOut, TIn>(Random random, Func<Random, TIn, TOut> function) : IGenerator<TOut, TIn>
{
    public TOut Generate(TIn input)
        => function.Invoke(random, input);
}