using Microsoft.Extensions.Logging;
using StellarMap.Domain.Galaxies;

namespace StellarMap.Application.Generation.Galaxies;

public class StarGenerator(
    ILogger<StarGenerator> logger,
    IGenerator<StarId> starIdGenerator,
    IGenerator<Name> starNameGenerator,
    IGenerator<StarClassification> starClassificationGenerator,
    IGenerator<StarSize, StarClassification> starSizeGenerator
) : IGenerator<Star>
{
    public Star Generate()
    {
        var id = starIdGenerator.Generate();
        var name = starNameGenerator.Generate();
        var classification = starClassificationGenerator.Generate();
        var size = starSizeGenerator.Generate(classification);

        logger.LogInformation("Generated Star: '{name}' ({id}) - {classification} - {size}", name, id, classification.Id, size);
        return new Star(id, name, classification, size);
    }
}