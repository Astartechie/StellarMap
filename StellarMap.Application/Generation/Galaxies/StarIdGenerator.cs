using Microsoft.Extensions.Logging;
using StellarMap.Domain.Galaxies;

namespace StellarMap.Application.Generation.Galaxies;

public class StarIdGenerator(ILogger<StarIdGenerator> logger) : IGenerator<StarId>
{
    public StarId Generate()
    {
        return StarId.Create(Guid.Empty);
    }

    private readonly ILogger<StarIdGenerator> _logger = logger;
}