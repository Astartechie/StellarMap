using System.Text;
using StellarMap.Domain.Galaxies;

namespace StellarMap.Application.Generation.Galaxies;

public class StarNameGenerator(
    IGenerator<int, (int, int)> intGenerator,
    IGenerator<Weighting> weightingGenerator,
    IProvider<MarkovChain<char>> markovChainProvider
) : IStarNameGenerator
{
    private static readonly (int, int) NameLengthRange = new(2, 4);
    private const char StartingCharacter = (char)0;

    public Name Generate()
    {
        var chain = markovChainProvider.Provide();

        var builder = new StringBuilder();
        var length = intGenerator.Generate(NameLengthRange);

        var lastCharacter = StartingCharacter;
        for (var index = 0; index < length; index++)
        {
            chain.TryGetWeightedList(lastCharacter, out var weightedList);
            var nextCharacter = weightedList.GetItemFromWeight(weightingGenerator.Generate());
            builder.Append(nextCharacter);
            lastCharacter = nextCharacter;
        }

        return Name.Create(builder.ToString());
    }
}