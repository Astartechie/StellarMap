using StellarMap.Application.Generation;

namespace StellarMap.UserInterface.Console.Providing.Galaxies;

internal class StarNameProvider(IReader<StarNameProvider> reader) : IProvider<MarkovChain<char>>
{
    public MarkovChain<char> Provide()
    {
        if (_markovChain != null) return _markovChain;
        var builder = new MarkovChainBuilder<char>();

        foreach (var line in reader.ReadLines())
        {
            var lastCharacter = (char)0;
            foreach (var nextCharacter in line)
            {
                builder.AddTransition(lastCharacter, nextCharacter, 1);
                lastCharacter = nextCharacter;
            }
        }

        _markovChain = builder.Build();
        return _markovChain;
    }

    private MarkovChain<char>? _markovChain = null;
}