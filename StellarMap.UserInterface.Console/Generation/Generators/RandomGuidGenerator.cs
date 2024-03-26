using StellarMap.Application.Generation.Generators;

namespace StellarMap.UserInterface.Console.Generation.Generators;

internal class RandomGuidGenerator(Random random) : IRandomGuidGenerator
{
    private const int GuidByteCount = 16;

    public Guid Generate()
    {
        var bytes = new byte[GuidByteCount];
        random.NextBytes(bytes);
        return new Guid(bytes);
    }
}